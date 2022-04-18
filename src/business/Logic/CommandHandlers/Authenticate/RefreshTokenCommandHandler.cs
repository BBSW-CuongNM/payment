using Shared.Config;
using Ultils.Helpers;

namespace Logic.CommandHandlers.Authenticate;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, CommonCommandResultHasData<UserLoginDataTransferObject>>
{
    private readonly ILogger<RefreshTokenCommandHandler> logger;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly ApplicationContext applicationContext;
    private readonly ErrorConfig errorConfig;
    private readonly JwtConfig jwtConfig;
    public RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> logger, 
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
    public async Task<CommonCommandResultHasData<UserLoginDataTransferObject>> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<UserLoginDataTransferObject>();

        try
        {
            var userToken = await applicationContext.UserTokens.FirstOrDefaultAsync(x => x.IsActive == true && x.RefreshToken == request.RefreshToken);
            if (userToken != null)
            {
                var user = await applicationContext.Users.FirstOrDefaultAsync(x => x.UserName == userToken.UserName);
                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var token = GenerateToken(user, userRoles);

                var dataResponse = new UserLoginDataTransferObject
                {
                    RefreshToken = GenerateRefreshToken(token),
                    TimeOut = jwtConfig.JwtTokenTimeOut ?? 10,
                    Token = token
                };

                var oldRefreshTokens = applicationContext.UserTokens.Where(x => x.RefreshToken == request.RefreshToken &&
                                       x.IsActive == true);

                await oldRefreshTokens.ForEachAsync(x =>
                {
                    x.IsActive = false;
                });

                /// Add new token
                applicationContext.UserTokens.Add(new UserToken()
                {
                    UserName = userToken.UserName,
                    CreatedAt = DateTime.UtcNow,
                    TokenValue = dataResponse.Token,
                    RefreshToken = dataResponse.RefreshToken,
                    IsActive = true
                });

                applicationContext.UserTokens.UpdateRange(oldRefreshTokens);

                await applicationContext.SaveChangesAsync();


                var jobId = BackgroundJob.Enqueue<RefreshTokenEventHandler>(
                                   x => x.CreateRefreshToken(new RefreshTokenEvent
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
                result.SetResult(false, "Lấy token thất bại.");

                return result;
            }
        }
        catch (Exception ex)
        {

            result.SetResult(false, ex.Message.ToString());
            return result;
        }

    }
    private string GenerateToken(User user, IList<string> roles = null)
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

