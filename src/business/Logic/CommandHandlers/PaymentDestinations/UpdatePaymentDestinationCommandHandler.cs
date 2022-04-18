namespace Logic.CommandHandlers;
internal class UpdatePaymentDestinationCommandHandler : IRequestHandler<UpdatePaymentDestinationCommand, CommonCommandResultHasData<PaymentDestination>>
{
    private readonly ILogger<UpdatePaymentDestinationCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public UpdatePaymentDestinationCommandHandler(ILogger<UpdatePaymentDestinationCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;

    }
    public async Task<CommonCommandResultHasData<PaymentDestination>> Handle(UpdatePaymentDestinationCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<PaymentDestination>();
        var paymentDestination = new PaymentDestination
        {
            Id = request.Id,
            ExternalId = request.ExternalId,
            Group = request.Group,
            ParentId = request.ParentId,
            Name = request.Name,
            OtherName = request.OtherName,
            Image = request.Image,
            SortIndex = request.SortIndex,
            PartnerId = request.PartnerId
        };
        applicationContext.PaymentDestionations.Update(paymentDestination);
        var comit = await applicationContext.SaveChangesAsync(cancellationToken);
        if (comit < 0)
        {
            result.SetResult(false, "");
            return result;
        }
        result.SetData(paymentDestination).SetResult(true, "");

        return result;
    }
}