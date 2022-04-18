namespace Data.Entities;
public class Payment : BaseEntity
{
    /// <summary>
    /// Mã yêu cầu thanh toán
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// Loại yêu cầu
    /// Pay: yêu cầu thanh toán
    /// Refund: yêu cầu hoàn tiền
    /// </summary>
    public string Command { get; set; } = string.Empty;
    /// <summary>
    /// Mã tham chiếu của partner -> payment
    /// </summary>
    public string ServiceReferenceCode { get; set; } = string.Empty;
    /// <summary>
    /// Mã tham chiếu của payment và các bên đối tác
    /// </summary>
    public string PaymentReferenceCode { get; set; } = string.Empty;
    /// <summary>
    /// Mã đơn hàng đang lưu trên hệ thống payment
    /// </summary>
    public string OrderId { get; set; } = string.Empty;
    /// <summary>
    /// Mã đối tác gửi yêu cầu thanh toán
    /// </summary>
    public string ServiceCode { get; set; } = string.Empty;
    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string ServiceOrderId { get; set; } = string.Empty;
    /// <summary>
    /// Số tiền thực hiện thanh toán hoặc hoàn trả
    /// </summary>
    public decimal Amount { get; set; }
    /// <summary>
    ///  Đơn vị tiền tệ
    /// </summary>
    public string? CurrencyUnit { get; set; }
    /// <summary>
    /// Cổng thanh toán bên thứ ba  : VnPay, Momo
    /// </summary>
    public string Via { get; set; } = string.Empty;
    /// <summary>
    /// Đích đến thanh toán cuối cùng
    /// </summary>
    public string Destination { get; set; } = string.Empty;
    /// <summary>
    /// Ngôn ngữ hiển thị giao diện
    /// </summary>
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// Nội dung thanh toán: “thanh toán đơn hàng 000000000001”
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    ///  URL thông báo kết quả giao dịch khi khách hàng kết thúc thanh toán.
    ///  Ví dụ: https://domain.vn/VnPayReturn
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
    /// <summary>
    /// Url thông báo kết quả thanh toán đến server
    /// </summary>
    public string IPNUrl { get; set; } = string.Empty;
    /// <summary>
    /// Trạng thái thanh toán
    /// </summary>
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// Mã lỗi
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;
    /// <summary>
    /// Nội dung lỗi
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

}
