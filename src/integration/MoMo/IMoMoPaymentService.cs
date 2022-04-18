using MoMo.Request;
using SharedModel;

namespace MoMo;
public interface IMoMoPaymentService
{
    PaymentRedirectLinkResult GetPayment(MoMoPaymentRequest request);
}
