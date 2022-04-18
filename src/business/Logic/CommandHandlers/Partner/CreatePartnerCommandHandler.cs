namespace Logic.CommandHandlers;

public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, CommonCommandResultHasData<Partner>>
{
    private readonly ILogger<CreatePartnerCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public CreatePartnerCommandHandler(ILogger<CreatePartnerCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
    }
    public async Task<CommonCommandResultHasData<Partner>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<Partner>();
        var partnerId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);

        var partner = new Partner
        {
            Id = partnerId,
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Website = request.Website,
            IPNUrl = request.IPNUrl,
            PartnerPublicKey = request.PartnerPublicKey,
            InternalPublicKey = request.InternalPublicKey,
            InternalPrivateKey = request.InternalPrivateKey
        };
        applicationContext.Partners.Add(partner);
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

