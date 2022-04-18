
namespace Logic.Events;

public class PaymentCallBackEvent : INotification
{
    public string? ServiceCode { get; set; }
    public Decimal Amount { get; set; }
    public string? PaymentType { get; set; }
    public string? TransactionStatus { get; set; }
    public string? TransactionNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? OrderId { get; set; }
    public string? ReferenceCode { get; set; }
    public string ? IPNUrl { get; set; }
}

