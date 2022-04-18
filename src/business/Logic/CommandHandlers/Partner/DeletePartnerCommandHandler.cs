namespace Logic.CommandHandlers;

public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, CommonCommandResultHasData<object>>
{
    private readonly ILogger<DeletePartnerCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public DeletePartnerCommandHandler(ILogger<DeletePartnerCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
    }
    public async Task<CommonCommandResultHasData<object>> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<object>();
        var partner = await applicationContext.Partners.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (partner == null)
        {
            result.SetResult(false, "");
            return result;
        }
        applicationContext.Partners.Remove(partner);
        var comit = await applicationContext.SaveChangesAsync(cancellationToken);
        if (comit < 0)
        {
            result.SetResult(false, "");
            return result;
        }
        else result.SetData(true).SetResult(true, "");

        return result;
    }
}

