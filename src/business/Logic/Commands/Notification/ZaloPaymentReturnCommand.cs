namespace Logic.Commands;
public class ZaloPaymentReturnCommand : IRequest<CommonCommandResult>
{
    public long AppId { get; set; }
    public string AppTransId { get; set; } = string.Empty;
    public long AppTime { get; set; }
    public string AppUser { get; set; } = string.Empty;
    public long Amount { get; set; }
    public string EmbedData { get; set; } = string.Empty;
    public string Item { get; set; } = string.Empty;
    public long ZPTransId { get; set; }
    public long ServerTime { get; set; }
    public int Channel { get; set; }
    public string MerchantUserId { get; set; } = string.Empty;
    public long UserFeeAmount { get; set; }
    public long DiscountAmount { get; set; }
}
