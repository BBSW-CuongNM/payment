namespace Logic.CommandHandlers;
public class ZaloPaymentReturnCommandHandler 
    : IRequestHandler<ZaloPaymentReturnCommand, CommonCommandResult>
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public ZaloPaymentReturnCommandHandler(ApplicationContext context,
        IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<CommonCommandResult> Handle(ZaloPaymentReturnCommand request, 
        CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();
        
        try
        {

        }
        catch(Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}
