using Ultils.Enum;
using Ultils.Helpers;

namespace Logic.CommandHandlers;
public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, CommonCommandResultHasData<OrderDto>>
{
    private readonly ILogger<UpdateOrderCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly HashedDataConfig hashedDataConfig;
   

    public UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger,
         ApplicationContext applicationContext,
         IOptions<HashedDataConfig> hashedDataConfig
         

     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.hashedDataConfig = hashedDataConfig.Value;
      
    }
    public async Task<CommonCommandResultHasData<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<OrderDto>();
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
                result.SetResult(false, "Đơn hàng đã thanh toán, không thể chỉnh sửa.");
                return result;
            }
            if (order.Status == Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.UNFINISHED))
            {
                result.SetResult(false, "Đơn hàng đang thanh toán, không thể chỉnh sửa.");
                return result;
            }
            if (order.Status == Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT))
            {
                var orderUpdate = new Order
                {
                    Id = order.Id,
                    ServiceCode = request!.Data!.ServiceCode,
                    ServiceOrderId = request.Data.ServiceOrderId,
                    OrderDate = request.Data.OrderDate,
                    Amount = request.Data.Amount,
                    Paid = 0,
                    Balance = request.Data.Amount,
                    IsInstallmentAllowed = request.Data.IsInstallmentAllowed,
                    Note = request.Data.Note,
                    Status = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT) ?? String.Empty,
                };
                applicationContext.Orders.Update(orderUpdate);

                // return data
                var comit = await applicationContext.SaveChangesAsync();
                if (comit < 0)
                {
                    result.SetResult(false, "Lỗi không thể cập nhật đơn hàng");
                    return result;
                }
                else result.SetData(new OrderDto { OrderReferenceCode = order.Id }).SetResult(true, "");
            }
            // save data
        }
        else
            result.SetResult(false, "Không thể xác thực chữ ký");
        return result;
    }
}
