namespace Data.Entities;
public class Order : BaseEntity
{
    /// <summary>
    /// Mã đơn hàng do hệ thống payment sinh
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Mã order service payment cấp cho đối tác
    /// </summary>
    public string ServiceCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string ServiceOrderId { get; set; } = string.Empty;
    
    /// <summary>
    /// Ngày đặt hàng
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    /// <summary>
    /// Tổng số tiền
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Số tiền đã thanh toán
    /// </summary>
    public decimal Paid { get; set; }
    
    /// <summary>
    /// Số còn lại
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Order cho phép thanh toán nhiều lần
    /// true (1): cho phép thanh toán nhiều lần
    /// false (0): không cho phép thanh toán nhiều lần
    /// </summary>
    public bool IsInstallmentAllowed { get; set; }

    /// <summary>
    /// Ghi chú đơn hàng
    /// </summary>
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Trạng thái đơn hàng
    /// Wait: Chờ thanh toán
    /// Paid: Hoàn tất thanh toán
    /// Unfinished:Thanh toán dở dang (trường hợp order thanh toán nhiều lần)
    /// </summary>
    public string Status { get; set; } = string.Empty;
}
