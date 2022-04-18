namespace Shared.ViewModels;
public class LoginViewModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Remember { get; set; } = false;
    public string ReturnUrl { get; set; } = string.Empty;
}