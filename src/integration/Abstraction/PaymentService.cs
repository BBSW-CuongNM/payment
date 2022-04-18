using Abstraction.Model;
using MoMo;
using SharedModel;

namespace Abstraction;
public class PaymentService : IPaymentService
{
    private readonly IVNPayPaymentService vnpayPaymentService;
    private readonly IMoMoPaymentService moMoPaymentService;

    public PaymentService(IVNPayPaymentService vnpayPaymentService,
        IMoMoPaymentService moMoPaymentService)
    {
        this.vnpayPaymentService = vnpayPaymentService;
        this.moMoPaymentService = moMoPaymentService;
    }

    public PaymentRedirectLinkResult GetPayment(PaymentRequestModel request, string partner)
    {
        var result = new PaymentRedirectLinkResult();
        switch (partner)
        {
            case "VNPAY":
                var vnpayRequest = request.ToVNPayPaymentRequest();
                result = vnpayPaymentService.GetPayment(vnpayRequest);
                break;
            case "MOMO":
                var momoRequest = request.ToMoMoPaymentRequest();
                result = moMoPaymentService.GetPayment(momoRequest);
                break;
            case "HDBANK":
                break;
            case "ZALOPAY":
                break;
        }
        return result;
    }
}

