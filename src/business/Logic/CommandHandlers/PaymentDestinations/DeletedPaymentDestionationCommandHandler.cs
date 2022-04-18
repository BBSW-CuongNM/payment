namespace Logic.CommandHandlers;

public class DeletedPaymentDestionationCommandHandler : IRequestHandler<DeletePaymentDestinationCommand, CommonCommandResult>
{
    private readonly ILogger<DeletedPaymentDestionationCommandHandler> logger;
    private readonly ApplicationContext applicationContext;

    public DeletedPaymentDestionationCommandHandler(ILogger<DeletedPaymentDestionationCommandHandler> logger,
         ApplicationContext applicationContext
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;

    }
    public async Task<CommonCommandResult> Handle(DeletePaymentDestinationCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        var paymentDestination = applicationContext.PaymentDestionations.Where(x => x.Id == request.Id).FirstOrDefault();
        if (paymentDestination == null)
        {
            result.SetResult(false, "");
            return result;
        }
        applicationContext.PaymentDestionations.Remove(paymentDestination);
        var comit = await applicationContext.SaveChangesAsync(cancellationToken);
        if (comit < 0)
        {
            result.SetResult(false, "");
            return result;
        }
        result.SetResult(true, "");

        return result;
    }
}

