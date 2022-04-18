using Ultils.Helpers;

namespace Logic.CommandHandlers;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, CommonCommandResult>
{
    private readonly ILogger<DeleteOrderCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly HashedDataConfig hashedDataConfig;

    public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger,
         ApplicationContext applicationContext,
         IOptions<HashedDataConfig> hashedDataConfig
     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.hashedDataConfig = hashedDataConfig.Value;
    }
    public async Task<CommonCommandResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();
        //
        var partner = await applicationContext.Partners.FirstOrDefaultAsync(x => x.Id == request!.Data!.ServiceCode);
        // verify hashed data
        var verify = SecurityHelper.VerifyHash(partner!.PartnerPublicKey ?? string.Empty, Convert.FromBase64String(request?.HashedData ?? string.Empty), Convert.FromBase64String(request?.Signature ?? string.Empty));
        if (verify)
        {
            var order = await applicationContext.Orders.FirstOrDefaultAsync(x => x.ServiceCode == request!.Data!.ServiceCode
                                                                         && x.ServiceOrderId == request.Data.ServiceOrderId);
            if (order!.Status == Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.PAID))
            {
                result.SetResult(false, "Đơn hàng đã thanh toán, không thể xóa.");
                return result;
            }
            if (order.Status == Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.UNFINISHED))
            {
                result.SetResult(false, "Đơn hàng đang thanh toán, không thể xóa.");
                return result;
            }
            if (order.Status == Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT))
            {

                applicationContext.Orders.Remove(order);

                // return data
                var comit = await applicationContext.SaveChangesAsync();
                if (comit < 0)
                {
                    result.SetResult(false, "Lỗi không thể xóa đơn hàng");
                    return result;
                }
                else result.SetResult(true, "");
            }
            // save data
        }
        else
            result.SetResult(false, "Không thể xác thực chữ ký");
        return result;
    }
}

