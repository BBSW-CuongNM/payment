
namespace Logic.Commands;
public class CreateOrderCommand : IRequest<CommonCommandResultHasData<OrderDto>>
{
    public CreateOrderData? Data { get; set; }
    public string? HashedData { get; set; }
    public string? Signature { get; set; }

}
public class CreateOrderData
{
    public string ServiceCode { get; set; } = string.Empty;

    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Ngày đặt hàng
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Tổng số tiền
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Order cho phép thanh toán nhiều lần
    /// true (1): cho phép thanh toán nhiều lần
    /// false (0): không cho phép thanh toán nhiều lần
    /// </summary>
    public bool IsInstallmentAllowed { get; set; } = false;

    /// <summary>
    /// Ghi chú đơn hàng
    /// </summary>
    public string Note { get; set; } = string.Empty;
};