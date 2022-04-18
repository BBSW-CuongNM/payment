using Ultils.Enum;
using Ultils.Helpers;

namespace Logic.CommandHandlers;
public class CreateOrderTransferCommandHandler : IRequestHandler<CreateOrderTransferCommand, CommonCommandResultHasData<OrderDto>>
{
    private readonly ILogger<CreateOrderTransferCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly HashedDataConfig hashedDataConfig;

    public CreateOrderTransferCommandHandler(ILogger<CreateOrderTransferCommandHandler> logger,
         ApplicationContext applicationContext,
         IOptions<HashedDataConfig> hashedDataConfig


     )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.hashedDataConfig = hashedDataConfig.Value;
    }
    public async Task<CommonCommandResultHasData<OrderDto>> Handle(CreateOrderTransferCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<OrderDto>();
        //
        var partner = await applicationContext.Partners.FirstOrDefaultAsync(x => x.Id == request.ServiceCode);
        // verify hashed data

        var orderIsExitst = await applicationContext.Orders.FirstOrDefaultAsync(x => x.ServiceCode == request.ServiceCode
                                                                     && x.ServiceOrderId == request.OrderId
                                                                     && x.IsDeleted == false);
        if (orderIsExitst == null)
        {
            var order = new Order
            {
                Id = request.Id?? string.Empty,
                ServiceCode = request.ServiceCode??string.Empty,
                ServiceOrderId = request.OrderId??string.Empty,
                OrderDate = request.OrderDate.ToUniversalTime(),
                Amount = request.Amount,
                Paid = 0,
                Balance = request.Amount,
                IsInstallmentAllowed = request.IsInstallmentAllowed,
                Note = request.Note,
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
        return result;
    }
}
