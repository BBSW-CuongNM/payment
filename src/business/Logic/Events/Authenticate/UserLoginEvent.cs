namespace Logic.Events.Authenticate;

public class UserLoginEvent
{
    public string? UserName { get; set; }
    public string? TokenValue { get; set; }
    public string? RefreshToken { get; set; }
    public bool? IsActive { get; set; }
    public string? FirebaseToken { get; set; }
}

