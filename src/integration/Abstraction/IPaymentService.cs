using Abstraction.Model;
using SharedModel;

namespace Abstraction;
public interface IPaymentService
{
    public PaymentRedirectLinkResult GetPayment(PaymentRequestModel Request, string partner);
}

