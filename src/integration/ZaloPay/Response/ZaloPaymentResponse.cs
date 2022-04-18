namespace ZaloPay.Response;
public class ZaloPaymentResponse
{
    /// <summary>
    /// Redirect về url này sau khi thanh toán trên cổng ZaloPay 
    /// (override redirect url lúc đăng ký app với ZaloPay)
    /// </summary>
    public string RedirectUrl { get; set; } = string.Empty;
    /// <summary>
    /// Thêm thông tin hiển thị ở phần Quản lý giao dịch chi tiết trên Merchant site, nếu cột chưa tồn tại cần vào phần Cài đặt hiển thị dữ liệu để cấu hình
    /// Lưu ý: đối với thanh toán Offline cần thiết truyền các thông tin như branch_id, store_id, store_name, ...
    /// </summary>
    public string ColumnInfo { get; set; } = string.Empty;
    /// <summary>
    /// Dùng để triển khai chương trình khuyến mãi
    /// </summary>
    public string PromotionInfo { get; set; } = string.Empty;
    /// <summary>
    /// - Mã thông tin thanh toán.
    /// - Chỉ truyền khi đối tác cần nhận tiền đối soát về nhiều tài khoản khác nhau.
    /// - Hệ thống ZaloPay sẽ tạo ra một mã Thanh toán 
    /// (tương ứng với mỗi Tài khoản ngân hàng đối tác cung cấp) và gởi lại cho đối tác thiết lập.)
    /// </summary>
    public string ZipPaymentId { get; set; } = string.Empty;
}
