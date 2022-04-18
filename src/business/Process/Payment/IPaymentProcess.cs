
namespace Process;

public interface IPaymentProcess
{
    Task<PaymentProcessCallBackRespone> PaymentCallBack(PaymentCallBackModel request,  string partnerId);
}

