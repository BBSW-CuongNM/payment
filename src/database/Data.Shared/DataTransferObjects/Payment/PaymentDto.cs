namespace Data.Shared.DataTransferObjects;
public class PaymentDto
{
    public string? PaymentUrl { get; set; }
    public int PaymentTimeoutAfterMinutes { get; set; }
}
