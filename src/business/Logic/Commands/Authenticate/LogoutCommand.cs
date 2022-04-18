namespace Logic.Commands;
public class LogoutCommand : IRequest<CommonCommandResult>
{
    public string? RefreshToken { get; set; }
}
