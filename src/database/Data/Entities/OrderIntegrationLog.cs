namespace Data.Entities;
public class OrderIntegrationLog
{
    public int Id { get; set; }
    /// <summary>
    /// Mã order service payment cấp cho đối tác
    /// </summary>
    public string OrderServiceCode { get; set; } = string.Empty;
    /// <summary>
    /// Mã đơn hàng do hệ thống payment sinh
    /// </summary>
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string ServiceOrderId { get; set; } = string.Empty;
    /// <summary>
    /// Json Request
    /// </summary>
    public string Request { get; set; } = string.Empty;
    /// <summary>
    /// Kết quả tích hợp
    /// </summary>
    public string Result { get; set; } = string.Empty;
}
