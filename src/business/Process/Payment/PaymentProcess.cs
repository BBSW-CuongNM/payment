using Data.Contexts;
using Newtonsoft.Json;
using Ultils.Extensions;

namespace Process;

public class PaymentProcess : IPaymentProcess
{
    private readonly HttpClient httpClient;
    private readonly ApplicationContext applicationContext;
    public PaymentProcess(HttpClient httpClient, ApplicationContext applicationContext)
    {
        this.httpClient = httpClient;
        this.applicationContext = applicationContext;
    }
    public Task<PaymentProcessCallBackRespone> PaymentCallBack(PaymentCallBackModel request, string IPNUrl)
    {
        var json = JsonConvert.SerializeObject(request);
        var result = new PaymentProcessCallBackRespone();

        Dictionary<string, string> header = new();
        return httpClient.ExecutePost<PaymentProcessCallBackRespone>(
              url: $"{IPNUrl}",
              data: request
              );
    }
}

