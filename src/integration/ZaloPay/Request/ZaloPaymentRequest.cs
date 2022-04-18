namespace ZaloPay.Request;
public class ZaloPaymentRequest
{
    /// <summary>
    /// Định danh ứng dụng đã được cấp bởi ZaloPay
    /// </summary>
    public int AppId { get; set; }
    /// <summary>
    /// Thông tin người dùng như id/username/số điện thoại/email của user
    /// </summary>
    public string AppUser { get; set; } = string.Empty;
    /// <summary>
    /// Thời gian tạo đơn hàng (unix timestamp in milisecond). 
    /// Thời gian tính đến milisecond, lấy theo current time và không quá 15 phút so với thời điểm thanh toán
    /// </summary>
    public long AppTime { get; set; }
    /// <summary>
    /// Giá trị của đơn hàng VND
    /// </summary>
    public long Amount { get; set; }
    /// <summary>
    /// Mã giao dịch của đơn hàng. Mã giao dịch phải bắt đầu theo format yymmdd của ngày hiện tại. 
    /// Mã giao dịch nên theo format yymmdd_Mã đơn hàng thanh toán
    /// Lưu ý: yymmdd phải đúng TimeZone Vietnam(GMT+7) (Vì các giao dịch đối soát theo ngày giờ Việt Nam)
    /// </summary>
    public string AppTransId { get; set; } = string.Empty;
    /// <summary>
    /// Dữ liệu riêng của đơn hàng. 
    /// Dữ liệu này sẽ được callback lại cho AppServer khi thanh toán thành công (Nếu không có thì để chuỗi rỗng)
    /// </summary>
    public string EmbedData { get; set; } = string.Empty;
    /// <summary>
    /// Item của đơn hàng, do ứng dụng tự định nghĩa
    /// </summary>
    public string Item { get; set; } = string.Empty;
    /// <summary>
    /// Thông tin chứng thực của đơn hàng, xem cách tạo thông tin chứng thực cho đơn hàng
    /// </summary>
    public string Mac { get; set; } = string.Empty;
    /// <summary>
    /// Mã ngân hàng, xem cách lấy danh sách các ngân hàng được hỗ trợ
    /// </summary>
    public string BankCode { get; set; } = string.Empty;
    /// <summary>
    /// Thông tin mô tả về dịch vụ đang được thanh toán dùng để hiển thị cho user trên app ZaloPay và trên tool quản lý Merchant
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Số điện thoại của người dùng
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Email của người dùng
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Địa chỉ của người dùng
    /// </summary>
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// Định danh dịch vụ / nhóm dịch vụ đối tác đăng ký với ZaloPay (chỉ áp dụng với một số đối tác đặc biệt)
    /// </summary>
    public string SubAppId { get; set; } = string.Empty;
}
