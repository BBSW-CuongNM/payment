namespace Logic.Commands;
public class RefreshTokenCommand : IRequest<CommonCommandResultHasData<UserLoginDataTransferObject>>
{
    public string? RefreshToken { get; set; }
}
