
namespace Logic.Commands;
public class CreateOrderTransferCommand : IRequest<CommonCommandResultHasData<OrderDto>>
{
    public string? Id { get; set; }
    public string? ServiceCode { get; set; } 

    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string? OrderId { get; set; } 

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

}