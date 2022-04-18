namespace Logic.Commands;
public class LoginCommand : IRequest<CommonCommandResultHasData<UserLoginDataTransferObject>>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
