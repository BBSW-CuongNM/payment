using Microsoft.Extensions.Options;
using MoMo.Config;
using MoMo.Helpers;
using MoMo.Request;
using MoMo.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedModel;
using System.Net.Http;
using Ultils.Extensions;

namespace MoMo;
public class MoMoPaymentService : IMoMoPaymentService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly MoMoConfig config;

    public MoMoPaymentService(IHttpClientFactory httpClientFactory,
        IOptions<MoMoConfig> config)
    {
        this.httpClientFactory = httpClientFactory;
        this.config = config.Value;
    }

    public PaymentRedirectLinkResult GetPayment(MoMoPaymentRequest request)
    {
        var result = new PaymentRedirectLinkResult();
        var client = httpClientFactory.CreateClient();
        
        try
        {
            var rawHash = "accessKey=" + config.AccessKey +
                "&amount=" + request.Amount +
                "&extraData=" + request.ExtraData +
                "&ipnUrl=" + request.IPNUrl +
                "&orderId=" + request.OrderId +
                "&orderInfo=" + request.OrderInfo +
                "&partnerCode=" + request.PartnerCode +
                "&redirectUrl=" + request.RedirectUrl +
                "&requestId=" + request.RequestId +
                "&requestType=" + request.RequestType;
            request.Signature = MoMoSecurityHelper.SignSHA256(rawHash, config.SecretKey);

            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            });

            var response = client.ExecutePost<MoMoPaymentResponse>(config.APIEndPoint, requestData).Result;

            if(response.ResultCode == 0)
            {
                result.IsSuccess = true;
                result.Message = response.Message;
                result.RedirectUrl = response.PayUrl;
            }
            else
            {
                result.Message = response.Message;
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }
        
        return result;
    }
}
