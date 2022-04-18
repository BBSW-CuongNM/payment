
using Shared.Config;

namespace Logic.CommandHandlers.Authenticate;
public class LoginCommandHandler 
    : IRequestHandler<LoginCommand, CommonCommandResultHasData<UserLoginDataTransferObject>>
{
    private readonly ILogger<LoginCommandHandler> logger;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly ApplicationContext applicationContext;
    private readonly ErrorConfig errorConfig;
    private readonly JwtConfig jwtConfig;
    
    public LoginCommandHandler(ILogger<LoginCommandHandler> logger, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ApplicationContext applicationContext,
            IOptions<JwtConfig> jwtConfig,
            IOptions<ErrorConfig> errorConfig)
    {
        this.logger = logger;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.applicationContext = applicationContext;
        this.errorConfig = errorConfig.Value;
        this.jwtConfig = jwtConfig.Value;
    }

    public async Task<CommonCommandResultHasData<UserLoginDataTransferObject>> Handle(LoginCommand request,
            CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<UserLoginDataTransferObject>();

        try
        {
            var user = await userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);

            if (user == null)
            {
                user = await applicationContext.Users.Where(x => x.PhoneNumber == request.UserName).FirstOrDefaultAsync();
                if (user == null)
                {
                    result.SetResult(false, errorConfig.GetByKey("InvalidAuthenticate"));
                    return result;
                }
            }

            if (!user.Enable)
            {
                result.SetResult(false, errorConfig.GetByKey("LockedAccount"));
                return result;
            }

            var login = await signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true).ConfigureAwait(false);

            if (login.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

                var token = GenerateToken(user, userRoles);

                var dataResponse = new UserLoginDataTransferObject
                {
                    RefreshToken = GenerateRefreshToken(token),
                    TimeOut = jwtConfig.JwtTokenTimeOut ?? 10,
                    Token = token
                };

                var jobId = BackgroundJob.Enqueue<LoginEventHandler>(
                     x => x.CreateUserToken(new UserLoginEvent
                     {
                         UserName = user.UserName,
                         TokenValue = dataResponse.Token,
                         RefreshToken = dataResponse.RefreshToken,
                         IsActive = true,
                         FirebaseToken = ""
                     }));

                result.SetData(dataResponse).SetResult(true, "");
                return result;
            }
            else
            {
                result.SetResult(false, errorConfig.GetByKey("LoginFail"));
                return result;
            }
        }
        catch (Exception ex)
        {

            result.SetResult(false, ex.Message.ToString());
            return result;
        }

    }

    private string GenerateToken(User user, IList<string>? roles = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfig.SecretKey ?? String.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id) ,
                    new Claim("roles", roles?.Count > 0 ? string.Join("$", roles) : string.Empty)
                }),
            Expires = DateTime.Now.AddMinutes(jwtConfig.JwtTokenTimeOut ?? 5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string GenerateRefreshToken(string token) => SecurityHelper.MD5(token);
}

