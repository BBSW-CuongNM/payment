using Ultils.Enum;
using Ultils.Helpers;

namespace Logic.CommandHandlers;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CommonCommandResultHasData<OrderDto>>
{
    private readonly ILogger<CreateOrderCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly HashedDataConfig hashedDataConfig;

    public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
         ApplicationContext applicationContext,
         IOptions<HashedDataConfig> hashedDataConfig


     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.hashedDataConfig = hashedDataConfig.Value;
    }
    public async Task<CommonCommandResultHasData<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<OrderDto>();
        //
        var partner = await applicationContext.Partners.FirstOrDefaultAsync(x => x.Id == request!.Data!.ServiceCode);
        // verify hashed data
        var verify =  SecurityHelper.VerifyHash(partner.PartnerPublicKey ?? string.Empty, Convert.FromBase64String(request?.HashedData ?? string.Empty), Convert.FromBase64String(request?.Signature ?? string.Empty));
        if (verify)
        {
            var orderIsExitst = await applicationContext.Orders.FirstOrDefaultAsync(x => x.ServiceCode == request!.Data!.ServiceCode
                                                                         && x.ServiceOrderId == request.Data.OrderId
                                                                         && x.IsDeleted == false);
            if (orderIsExitst == null)
            {
                var orderId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
                var order = new Order
                {
                    Id = orderId,
                    ServiceCode = request!.Data!.ServiceCode,
                    ServiceOrderId = request.Data.OrderId,
                    OrderDate = request.Data.OrderDate.ToUniversalTime(),
                    Amount = request.Data.Amount,
                    Paid = 0,
                    Balance = request.Data.Amount,
                    IsInstallmentAllowed = request.Data.IsInstallmentAllowed,
                    Note = request.Data.Note,
                    Status = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT) ?? String.Empty,
                };
                // save data
                await applicationContext.Orders.AddAsync(order);

                // return data
                var comit = await applicationContext.SaveChangesAsync();
                if (comit < 0)
                {
                    result.SetResult(false, "Lỗi không thể lưu được order");
                    return result;
                }
                else result.SetData(new OrderDto { OrderReferenceCode = order.Id }).SetResult(true, "");
            }
            else
                result.SetResult(false, "Đơn hàng đã tồn tại, không thể thêm mới.");
        }
        else
            result.SetResult(false, "Không thể xác thực chữ ký");
        return result;
    }
}
