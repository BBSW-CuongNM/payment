namespace Logic.EventHandlers;

public class PaymentCallBackEventHandler : INotificationHandler<PaymentCallBackEvent>
{
    private readonly ILogger<PaymentCallBackEventHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly IPaymentProcess paymentProcess;
    private readonly HashedDataConfig hashedDataConfig;

    public PaymentCallBackEventHandler(ILogger<PaymentCallBackEventHandler> logger,
         ApplicationContext applicationContext,
         IPaymentProcess paymentProcess,
         IOptions<HashedDataConfig> hashedDataConfig
         )
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.paymentProcess = paymentProcess;
        this.hashedDataConfig = hashedDataConfig.Value;
    }
    public Task Handle(PaymentCallBackEvent request, CancellationToken cancellationToken)
    {
        try
        {
            var data = new DataCallBack
            {
                ServiceCode = request.ServiceCode ?? String.Empty,
                Amount = request.Amount,
                PaymentType = request.PaymentType ?? String.Empty,
                TransactionStatus = request.TransactionStatus ?? String.Empty,
                TransactionDate = request.TransactionDate,
                OrderId = request.OrderId ?? String.Empty,
                ReferenceCode = request.ReferenceCode ?? String.Empty
            };
            EncryptedDataQueryRequest security = new EncryptedDataQueryRequest
            {
                RsaSenderPrivateKey = hashedDataConfig.RsaSenderPrivateKey,
                RsaReceiverPublicKey = hashedDataConfig.RsaSenderPublicKey,
                AesKey = hashedDataConfig.AesKey,
                AesIV = hashedDataConfig.AesIV,
                TextToEncrypt = JsonConvert.SerializeObject(data)
            };
            var securityResult = HashAndSignPaymentRequest(security);
            // Call API 
            if (securityResult.Success)
            {
                var requestCallBack = new PaymentCallBackModel
                {
                    Data = data,
                    HashedData = securityResult!.Data!.RsaEncryptedAesKey ?? string.Empty,
                    Signature = securityResult!.Data!.RsaSignature ?? string.Empty,
                };
                var ipnUrl = request.IPNUrl;
                if (string.IsNullOrEmpty(ipnUrl))
                {
                    var partner = applicationContext.Partners.FirstOrDefault(x => x.Id == request.ServiceCode);
                }

                var response = paymentProcess.PaymentCallBack(requestCallBack, ipnUrl ?? String.Empty);
                if (!response.Result.IsSuccess)
                {

                    logger.LogError("Failure call ipn {0} : {1}:{2}", request.OrderId, request.ServiceCode, response.Result.Message);
                }
                else
                {
                    logger.LogInformation("Success call ipn {0} : {1}:{2}", request.OrderId, request.ServiceCode, response.Result.Message);

                }
            }
            else
            {
                logger.LogError("Failure create hash {0} : {1} ", request.OrderId, request.ServiceCode);

            }
        }
        catch(Exception ex)
        {
            logger.LogError("Failure  {0} : {1}:{2} ", request.OrderId, request.ServiceCode,ex.Message.ToString());
        }
        
        return Task.CompletedTask;
    }
    public CommonCommandResultHasData<EncryptedDataResponse> HashAndSignPaymentRequest(EncryptedDataQueryRequest security)
    {
        try
        {
            byte[] aesEncrypted = SecurityHelper.EncryptAes(security.TextToEncrypt ?? string.Empty,
                Convert.FromBase64String(security.AesKey ?? string.Empty),
                Convert.FromBase64String(security.AesIV ?? string.Empty));

            byte[] rsaEncrypted = SecurityHelper.EncryptData(security.RsaSenderPrivateKey ?? string.Empty,
                Convert.FromBase64String(security.AesKey ?? string.Empty));

            byte[] rsaSigned = SecurityHelper.HashAndSign(security.RsaSenderPrivateKey ?? string.Empty, rsaEncrypted);

            return new CommonCommandResultHasData<EncryptedDataResponse>
            {
                Data = new EncryptedDataResponse()
                {
                    AesEncryptedData = Convert.ToBase64String(aesEncrypted),
                    RsaEncryptedAesKey = Convert.ToBase64String(rsaEncrypted),
                    RsaSignature = Convert.ToBase64String(rsaSigned),
                },
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new CommonCommandResultHasData<EncryptedDataResponse>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}

