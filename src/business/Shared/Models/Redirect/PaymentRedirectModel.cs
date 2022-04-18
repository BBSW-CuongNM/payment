namespace Shared.Models;
public class PaymentRedirectModel
{
    public string Id { get; set; } = string.Empty;
    public string Command { get; set; } = string.Empty;
    public string ServiceReferenceCode { get; set; } = string.Empty;
    public string PaymentReferenceCode { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string ServiceCode { get; set; } = string.Empty;
    public string ServiceOrderId { get; set; } = string.Empty;
    public decimal? Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}