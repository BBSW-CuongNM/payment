namespace SharedModel;
public class PaymentRedirectLinkResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
}
