namespace Shared.Models;
public class PaymentCallBackModel
{
    public DataCallBack? Data { get; set; }
    public string HashedData { get; set; } = String.Empty;
    public string Signature { get; set; } = String.Empty;
}

public class DataCallBack
{
    public string ServiceCode { get; set; } = String.Empty;
    public Decimal Amount { get; set; }
    public string PaymentType { get; set; } = String.Empty;
    public string TransactionStatus { get; set; } = String.Empty;
    public string TransactionNumber { get; set; } = String.Empty;
    public DateTime TransactionDate { get; set; }
    public string OrderId { get; set; } = String.Empty;
    public string ReferenceCode { get; set; } = String.Empty;
}
