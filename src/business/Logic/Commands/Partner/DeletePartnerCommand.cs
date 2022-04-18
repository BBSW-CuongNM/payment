namespace Logic.Commands;

public class DeletePartnerCommand : IRequest<CommonCommandResultHasData<object>>
{
    public string Id { get; set; } = string.Empty;
}

