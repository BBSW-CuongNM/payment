namespace Shared.Models;
public class PaymentResponeModel
{
    /// <summary>
    /// Redirect to web payment
    /// </summary>
    public string? PaymentUrl { get; set; }
    /// <summary>
    /// Deeplink for app
    /// </summary>
    public string? DeeplinkApp { get; set; }
    /// <summary>
    /// Thời gian tối đa cho 1 thanh toán
    /// </summary>
    public int PaymentTimeoutAfterMinutes { get; set; }
}
