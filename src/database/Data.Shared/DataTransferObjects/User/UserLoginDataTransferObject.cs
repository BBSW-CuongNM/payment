namespace Data.Shared.DataTransferObjects;
public class UserLoginDataTransferObject
{
    public string RefreshToken { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public int TimeOut { get; set; } 
}