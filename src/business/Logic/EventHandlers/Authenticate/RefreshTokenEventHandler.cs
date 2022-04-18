using Logic.Events.Authenticate;

namespace Logic.EventHandlers.Authenticate;
public class RefreshTokenEventHandler
{
    private readonly ILogger<RefreshTokenEventHandler> logger;
    private readonly ApplicationContext applicationContext;
    public RefreshTokenEventHandler(ILogger<RefreshTokenEventHandler> logger,
        ApplicationContext applicationContext
        )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
    }
    public async Task CreateRefreshToken(RefreshTokenEvent user)
    {
        var request = new UserToken
        {
            UserName = user.UserName,
            TokenValue = user.TokenValue,
            RefreshToken = user.RefreshToken,
            IsActive = true,
            FirebaseToken = ""
        };
        // Gen Hash & sign data 
        await applicationContext.UserTokens.AddAsync(request);
        var comit = await applicationContext.SaveChangesAsync();
        if (comit > 0)
        {
            logger.LogInformation("---Save Token success for UserName: {@UserName} ", user.UserName);
        }
        else
            logger.LogError("---Save Token fail for UserName: {@UserName} ", user.UserName);
        await Task.CompletedTask;
    }
}

