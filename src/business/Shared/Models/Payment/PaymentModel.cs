namespace Shared.Models;
public class PaymentModel
{
    public string Command { get; set; } = string.Empty;
    /// <summary>
    /// Mã tham chiếu của partner -> payment
    /// </summary>
    public string ReferenceCode { get; set; } = string.Empty;

    /// <summary>
    /// Mã đối tác gửi yêu cầu thanh toán
    /// </summary>
    public string ServiceCode { get; set; } = string.Empty;
    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string OrderId { get; set; } = string.Empty;
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
    public string PaymentVia { get; set; } = string.Empty;
    /// <summary>
    ///  URL thông báo kết quả giao dịch khi khách hàng kết thúc thanh toán. Ví dụ: https://domain.vn/VnPayReturn
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
    /// <summary>
    /// Đích đến thanh toán cuối cùng
    /// </summary>
    public string PaymentDestinationId { get; set; } = string.Empty;
    /// <summary>
    /// Ngôn ngữ hiển thị giao diện
    /// </summary>
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// Url thông báo kết quả thanh toán đến server
    /// </summary>
    public string IPNUrl { get; set; } = string.Empty;
    /// <summary>
    ///  Ngày tạo order
    /// </summary>
    public DateTime? CreateDate { get; set; }
    /// <summary>
    /// Mô tả lí do thanh toán
    /// </summary>
    public string? Description { get; set; }
}