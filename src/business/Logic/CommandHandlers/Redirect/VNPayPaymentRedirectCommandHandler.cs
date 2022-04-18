
using Shared.Config;
using VNPay.Config;
using VNPay.Helpers;

namespace Logic.CommandHandlers;
public class VNPayPaymentRedirectCommandHandler : IRequestHandler<VNPayPaymentRedirectCommand, string>
{
    private readonly ILogger<VNPayPaymentRedirectCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly VNPayConfig vnPayConfig;
    private readonly ErrorConfig errorConfig;

    public VNPayPaymentRedirectCommandHandler(ILogger<VNPayPaymentRedirectCommandHandler> logger,
         ApplicationContext applicationContext,
          IOptions<ErrorConfig> errorConfig,
          IOptions<VNPayConfig> vnPayConfig
)
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.vnPayConfig = vnPayConfig.Value;
        this.errorConfig = errorConfig.Value;
    }
    public async Task<string> Handle(VNPayPaymentRedirectCommand request, CancellationToken cancellationToken)
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

        var payment = await applicationContext.Payments.FirstOrDefaultAsync(x => x.Id == request.vnp_TxnRef);
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
        var responeData = request.ToSortedList(new VNPayCompareHelper());
        var isValidSignature = VNPaySecurityHelper.ValidateSignature(responeData, request.vnp_SecureHash ?? string.Empty,
                    vnPayConfig.HashSecret);
        if (isValidSignature)
        {
            if (request.vnp_ResponseCode == "00")
                paymentStatus = PaymentStatusConst.SUCCESS;
            redirectModel = new PaymentRedirectModel
            {
                Id = payment.Id,
                Command = payment.Command,
                ServiceReferenceCode = payment.ServiceReferenceCode,
                PaymentReferenceCode = request.vnp_TransactionNo ?? string.Empty,
                OrderId = payment.OrderId,
                ServiceCode = payment.ServiceCode,
                ServiceOrderId = payment.ServiceOrderId,
                Amount = request.vnp_Amount,
                Status = paymentStatus,
                ErrorMessage = String.Empty
            };
            var returnUrl = payment.ReturnUrl;
            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Substring(0, returnUrl.Length - 1);
            result = payment.ReturnUrl + "?" + redirectModel.GetQueryString();
        }
        else
        {
            redirectModel = new PaymentRedirectModel
            {
                Status = paymentStatus,
                ErrorMessage = errorConfig.GetByKey("InvalidSignature")
            };
            result = redirectModel.GetQueryString();
            return result;
        }   
        return result;
    }
}
