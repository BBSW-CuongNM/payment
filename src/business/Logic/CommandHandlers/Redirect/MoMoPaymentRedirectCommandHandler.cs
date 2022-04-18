
using MoMo.Config;
using MoMo.Helpers;
using Shared.Config;
using VNPay.Config;
using VNPay.Helpers;

namespace Logic.CommandHandlers;
public class MoMoPaymentRedirectCommandHandler : IRequestHandler<MoMoPaymentRedirectCommand, string>
{
    private readonly ILogger<MoMoPaymentRedirectCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly MoMoConfig momoConfig;
    private readonly ErrorConfig errorConfig;

    public MoMoPaymentRedirectCommandHandler(ILogger<MoMoPaymentRedirectCommandHandler> logger,
         ApplicationContext applicationContext,
          IOptions<ErrorConfig> errorConfig,
          IOptions<MoMoConfig> momoConfig
)
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.momoConfig = momoConfig.Value;
        this.errorConfig = errorConfig.Value;
    }
    public async Task<string> Handle(MoMoPaymentRedirectCommand request, CancellationToken cancellationToken)
    {
        var redirectModel = new PaymentRedirectModel();
        var result = string.Empty;
        var paymentStatus = PaymentStatusConst.FAILURE;
        if (request == null)
        {
            redirectModel = new PaymentRedirectModel
            {
                Status = paymentStatus,
                ErrorMessage = errorConfig.GetByKey("InvalidPaymentReturnURL")
            };
            result = redirectModel.GetQueryString();
            return result;
        }

        var rawHash = "accessKey=" + momoConfig.AccessKey +
               "&amount=" + request.Amount +
               "&extraData=" + request.ExtraData +
               "&message=" + request.Message +
               "&orderId=" + request.OrderId +
               "&orderInfo=" + request.OrderInfo +
               "&orderType=" + request.OrderType +
               "&partnerCode=" + request.PartnerCode +
               "&payType=" + request.PayType +
               "&requestId=" + request.RequestId +
               "&responseTime=" + request.ResponseTime +
               "&resultCode=" + request.ResultCode +
               "&transId=" + request.TransId;

        var signatureResponse = MoMoSecurityHelper.SignSHA256(rawHash, momoConfig.SecretKey);

        if (!signatureResponse.Equals(request.Signature))
        {
            redirectModel = new PaymentRedirectModel
            {
                Status = paymentStatus,
                ErrorMessage = errorConfig.GetByKey("InvalidSignature")
            };
            result = redirectModel.GetQueryString();
            return result;
        }
        var payment = await applicationContext.Payments.FirstOrDefaultAsync(x => x.Id == request.RequestId);

        if (payment == null)
        {
            redirectModel = new PaymentRedirectModel
            {
                Status = paymentStatus,
                ErrorMessage = errorConfig.GetByKey("InvalidPayment")
            };
            result = redirectModel.GetQueryString();
            return result;
        }
        if (request.ResultCode == 0)
            paymentStatus = PaymentStatusConst.SUCCESS;
        redirectModel = new PaymentRedirectModel
        {
            Id = payment.Id,
            Command = payment.Command,
            ServiceReferenceCode = payment.ServiceReferenceCode,
            PaymentReferenceCode = request.TransId??string.Empty,
            OrderId = payment.OrderId,
            ServiceCode = payment.ServiceCode,
            ServiceOrderId = payment.ServiceOrderId,
            Amount = request.Amount,
            Status = paymentStatus,
            ErrorMessage = String.Empty
        };
        var returnUrl = payment.ReturnUrl;
        if (returnUrl.EndsWith("/"))
            returnUrl = returnUrl.Substring(0, returnUrl.Length - 1);
        result = payment.ReturnUrl + "?" + redirectModel.GetQueryString();
        return result;

    }
}
