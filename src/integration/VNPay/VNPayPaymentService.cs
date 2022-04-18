using Abstraction;
using Microsoft.Extensions.Options;
using SharedModel;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Ultils.Extensions;
using Ultils.Helpers;
using VNPay.Config;
using VNPay.Helpers;
using VNPay.Request;

namespace VNPay;
public class VNPayPaymentService : IVNPayPaymentService
{
    private readonly VNPayConfig config;

    public VNPayPaymentService(IOptions<VNPayConfig> config)
    {
        this.config = config.Value;
    }

    public PaymentRedirectLinkResult GetPayment(VNPayPaymentRequest request)
    {
        StringBuilder data = new StringBuilder();

        var redirectUrl = config.Host;
        var requestData = request.ToPaymentRequestData(config);

        foreach (KeyValuePair<string, string> kv in requestData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(kv.Key + "=" + HttpUtility.UrlEncode(kv.Value) + "&");
            }
        }

        string queryString = data.ToString();
        string rawData = requestData.GetRawData();
        redirectUrl += "?" + queryString;
        string vnp_SecureHash = VNPaySecurityHelper.HmacSHA512(config.HashSecret,rawData);
        redirectUrl += "vnp_SecureHash=" + vnp_SecureHash;

        var result = new PaymentRedirectLinkResult()
        {
            IsSuccess = true,
            Message = String.Empty,
            RedirectUrl = redirectUrl,
        };

        return result;
    }
}
