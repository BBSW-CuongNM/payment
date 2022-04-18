namespace Logic.CommandHandlers;

public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, CommonCommandResultHasData<Partner>>
{
    private readonly ILogger<UpdatePartnerCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public UpdatePartnerCommandHandler(ILogger<UpdatePartnerCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
    }
    public async Task<CommonCommandResultHasData<Partner>> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<Partner>();

        var partner = new Partner
        {
            Id = request.Id,
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Website = request.Website,
            IPNUrl = request.IPNUrl,
            PartnerPublicKey = request.PartnerPublicKey,
            InternalPublicKey = request.InternalPublicKey,
            InternalPrivateKey = request.InternalPrivateKey
        };
        applicationContext.Partners.Update(partner);
        var comit = await applicationContext.SaveChangesAsync(cancellationToken);
        if (comit < 0)
        {
            result.SetResult(false, "");
            return result;
        }
        else result.SetData(partner).SetResult(true, "");

        return result;
    }
}

