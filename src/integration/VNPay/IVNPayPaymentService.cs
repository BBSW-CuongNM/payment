using SharedModel;

namespace Abstraction;
public interface IVNPayPaymentService
{
    public PaymentRedirectLinkResult GetPayment(VNPayPaymentRequest request);
}

