namespace Logic.CommandHandlers;

public class CreatePaymentDestinationCommandHandler : IRequestHandler<CreatePaymentDestinationCommand, CommonCommandResultHasData<PaymentDestination>>
{
    private readonly ILogger<CreatePaymentDestinationCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public CreatePaymentDestinationCommandHandler(ILogger<CreatePaymentDestinationCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;

    }
    public async Task<CommonCommandResultHasData<PaymentDestination>> Handle(CreatePaymentDestinationCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<PaymentDestination>();
        string IdKey = string.Empty;
        if (string.IsNullOrEmpty(request.PartnerId) )
            IdKey = request.ExternalId??string.Empty;
        else
            IdKey = String.Format("{0}_{1}", request.PartnerId, request.ExternalId);
        var paymentDestination = new PaymentDestination
        {
            Id = IdKey,
            ExternalId = request.ExternalId,
            Group = request.Group,
            ParentId = request.ParentId,
            Name = request.Name,
            OtherName = request.OtherName,
            Image = request.Image,
            SortIndex = request.SortIndex,
            PartnerId = request.PartnerId
        };
        applicationContext.PaymentDestionations.Add(paymentDestination);
        var comit = await applicationContext.SaveChangesAsync(cancellationToken);
        if (comit < 0)
        {
            result.SetResult(false, "");
            return result;
        }
        else result.SetData(paymentDestination).SetResult(true, "");

        return result;
    }
}

