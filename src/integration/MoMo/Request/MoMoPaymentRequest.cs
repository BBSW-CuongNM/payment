using MoMo.Helpers;

namespace MoMo.Request;
public class MoMoPaymentRequest
{
    public string PartnerCode { get; set; } = string.Empty;
    public string PartnerName { get; set; } = string.Empty;
    public string StoreId { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
    public long Amount { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string OrderInfo { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string IPNUrl { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public string ExtraData { get; set; } = string.Empty;
    public bool AutoCapture { get; set; }
    public string Lang { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;

    public string GetRawHash(string accessKey, string requestType)
    {
        string result = "accessKey=" + accessKey +
                "&amount=" + this.Amount +
                "&extraData=" + this.ExtraData +
                "&ipnUrl=" + this.IPNUrl +
                "&orderId=" + this.OrderId +
                "&orderInfo=" + this.OrderInfo +
                "&partnerCode=" + this.PartnerCode +
                "&redirectUrl=" + this.RedirectUrl +
                "&requestId=" + this.RequestId +
                "&requestType=" + requestType;
        return result;
    }

    public MoMoPaymentRequest SetSignature(string rawHash, string secretKey)
    {
        this.Signature = MoMoSecurityHelper.SignSHA256(rawHash, secretKey);
        return this;
    }
}
