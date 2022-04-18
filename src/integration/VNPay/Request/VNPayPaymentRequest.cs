using VNPay.Helpers;

namespace VNPay.Request;
public class VNPayPaymentRequest
{
    public string VNP_Version { get; set; } = string.Empty;
    public string VNP_Command { get; set; } = string.Empty;
    public string VNP_TmnCode { get; set; } = string.Empty;
    public string VNP_Locale { get; set; } = string.Empty;
    public string VNP_CurrCode { get; set; } = string.Empty;
    public string VNP_TxnRef { get; set; } = string.Empty;
    public string VNP_OrderInfo { get; set; } = string.Empty;
    public string VNP_OrderType { get; set; } = string.Empty;
    public decimal VNP_Amount { get; set; }
    public string VNP_ReturnUrl { get; set; } = string.Empty;
    public string VNP_IpAddr { get; set; } = string.Empty;
    public DateTime VNP_CreateDate { get; set; }
    public string VNP_BankCode { get; set; } = string.Empty;

    public SortedList<string, string> ToPaymentRequestData(VNPayConfig config)
    {
        var requestData = new SortedList<string, string>(new VNPayCompareHelper());

        requestData.AddData("vnp_Version", config.Version ?? string.Empty);
        requestData.AddData("vnp_Command", "pay");
        requestData.AddData("vnp_TmnCode", config.TMNCode ?? string.Empty);
        requestData.AddData("vnp_Locale", this.VNP_Locale ?? string.Empty);
        requestData.AddData("vnp_CurrCode", "VND");
        requestData.AddData("vnp_TxnRef", this.VNP_TxnRef ?? string.Empty);
        requestData.AddData("vnp_OrderInfo", string.IsNullOrEmpty(this.VNP_OrderInfo) ? string.Empty : this.VNP_OrderInfo);
        requestData.AddData("vnp_Amount", ((int)(this.VNP_Amount * 100)).ToString());
        requestData.AddData("vnp_ReturnUrl", this.VNP_ReturnUrl ?? string.Empty);
        requestData.AddData("vnp_IpAddr", this.VNP_IpAddr ?? string.Empty);
        requestData.AddData("vnp_CreateDate", this.VNP_CreateDate.ToString("yyyyMMddHHmmss"));

        if (!string.IsNullOrEmpty(this.VNP_BankCode))
        {
            requestData.AddData("vnp_BankCode", this.VNP_BankCode);
        }
        return requestData;
    }
}
