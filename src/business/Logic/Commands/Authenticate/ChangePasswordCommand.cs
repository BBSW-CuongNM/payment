namespace Logic.Commands;

public class ChangePasswordCommand : IRequest<CommonCommandResult>
{
    public string? UserName { get; set; }
    public string? OldPassWord { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}

