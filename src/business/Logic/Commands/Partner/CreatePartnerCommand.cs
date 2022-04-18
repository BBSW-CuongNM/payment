
namespace Logic.Commands;

public class CreatePartnerCommand : IRequest<CommonCommandResultHasData<Partner>>
{
    /// <summary>
    /// Tên gọi
    /// VD: Công ty cổ phần dịch vụ & giải pháp công nghệ GOTRUST
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Số điện thoại liên hệ
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Email liên hệ
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Website đối tác
    /// </summary>
    public string Website { get; set; } = string.Empty;
    /// <summary>
    /// IPN thống báo kết quả thanh toán
    /// </summary>
    public string IPNUrl { get; set; } = string.Empty;

    /// <summary>
    /// Public key của partner
    /// </summary>
    public string PartnerPublicKey { get; set; } = string.Empty;

    /// <summary>
    /// Public key của payment giao tiếp với partner
    /// </summary>
    public string InternalPublicKey { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string InternalPrivateKey { get; set; } = string.Empty;
}

