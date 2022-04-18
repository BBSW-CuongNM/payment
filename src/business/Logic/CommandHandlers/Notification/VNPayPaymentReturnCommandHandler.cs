
using VNPay.Config;
using VNPay.Helpers;

namespace Logic.CommandHandlers;
public class VNPayPaymentReturnCommandHandler : IRequestHandler<VNPayPaymentReturnCommand, VNPayPaymentReturnResponseModel>
{
    private readonly ILogger<CreatePaymentCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly IPaymentProcess paymentProcess;
    private readonly IMediator mediator;
    private readonly VNPayConfig vnPayConfig;
    private readonly HashedDataConfig hashedDataConfig;

    public VNPayPaymentReturnCommandHandler(ILogger<CreatePaymentCommandHandler> logger,
         ApplicationContext applicationContext, IPaymentProcess paymentProcess,
         IOptions<HashedDataConfig> hashedDataConfig,
         IOptions<VNPayConfig> vnPayConfig,
         IMediator mediator)
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.paymentProcess = paymentProcess;
        this.mediator = mediator;
        this.vnPayConfig = vnPayConfig.Value;
        this.hashedDataConfig = hashedDataConfig.Value;
    }
    public async Task<VNPayPaymentReturnResponseModel> Handle(VNPayPaymentReturnCommand request, CancellationToken cancellationToken)
    {
        var result = new VNPayPaymentReturnResponseModel();
        try
        {
            if (request != null)
            {
                var responeData = request.ToSortedList(new VNPayCompareHelper());
                var isValidSignature = VNPaySecurityHelper.ValidateSignature(responeData, request.vnp_SecureHash ?? string.Empty,
                            vnPayConfig.HashSecret);
                if (isValidSignature)
                {
                    var paymentInfor = await applicationContext.Payments.FirstOrDefaultAsync(x => x.Id == request.vnp_TxnRef);

                    if (paymentInfor != null)
                    {
                        var orderInfo = await applicationContext.Orders.FirstOrDefaultAsync(x => x.ServiceCode == paymentInfor.ServiceCode
                                                                                    && x.ServiceOrderId == paymentInfor.ServiceOrderId
                                                                                    && x.IsDeleted == false
                                                                                    && x.Status != Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.PAID));

                        string paymentStatus = PaymentStatusConst.FAILURE;
                        string orderStatus = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT) ?? string.Empty;
                        if (request.vnp_ResponseCode == "00")
                        {
                            if (paymentInfor.Status != Enum.GetName(typeof(PaymentStatusEnum), PaymentStatusEnum.COMPLETED))
                            {
                                paymentInfor.Status = Enum.GetName(typeof(PaymentStatusEnum), PaymentStatusEnum.COMPLETED) ?? String.Empty;
                                result.Message = "Confirm Success";
                                result.RspCode = "00";
                                paymentStatus = PaymentStatusConst.SUCCESS;
                                orderStatus = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.PAID) ?? string.Empty;
                                logger.LogInformation("Success");
                            }
                            else
                            {
                                result.Message = "Order already confirmed";
                                result.RspCode = "02";
                                logger.LogError("Failure {0} : {1}", result.RspCode, result.Message);
                            }

                            if (paymentInfor.Amount * 100 != request.vnp_Amount)
                            {
                                result.Message = "Invalid amount";
                                result.RspCode = "04";
                                logger.LogError("Failure {0} : {1}", result.RspCode, result.Message);
                            }
                        }
                        else
                        {
                            paymentInfor.Status = Enum.GetName(typeof(PaymentStatusEnum), PaymentStatusEnum.FAILURE) ?? String.Empty;
                            result.Message = "Confirm Fail";
                            result.RspCode = "00";
                            logger.LogError("Failure {0} : {1}", result.RspCode, result.Message);
                        }
                        paymentInfor!.PaymentReferenceCode = request.vnp_TransactionNo ?? string.Empty;
                        applicationContext.Payments.Update(paymentInfor);
                        if (orderInfo != null)
                        {
                            orderInfo.Status = orderStatus;
                            applicationContext.Orders.Update(orderInfo);

                        }
                        var comit = await applicationContext.SaveChangesAsync();
                        // gọi service code
                        if (comit < 0)
                        {
                            result.Message = "Unknown error";
                            result.RspCode = "99";
                            logger.LogError("Failure save {0} : {1}", result.RspCode, result.Message);
                        }
                        else
                        {
                            await mediator.Publish(new PaymentCallBackEvent
                            {
                                ServiceCode = paymentInfor.ServiceCode,
                                Amount = paymentInfor.Amount,
                                PaymentType = request.vnp_CardType ?? string.Empty,
                                TransactionStatus = paymentStatus,
                                TransactionNumber = request.vnp_TransactionNo,
                                TransactionDate = DateTime.ParseExact(request.vnp_PayDate, "yyyyMMddHHmmss", null),
                                OrderId = paymentInfor.ServiceOrderId,
                                ReferenceCode = paymentInfor.ServiceReferenceCode,
                                IPNUrl = paymentInfor.IPNUrl
                            });
                        }
                    }
                    else
                    {
                        result.Message = "Order Not Found";
                        result.RspCode = "01";
                        logger.LogError("Failure {0} : {1}", result.RspCode, result.Message);

                    }
                }
                else
                {

                    result.Message = "Invalid signature";
                    result.RspCode = "97";
                }
            }
        }
        catch (Exception ex)
        {
            result.Message = "Unknown error";
            result.RspCode = "99";
            logger.LogError("Failure  {0} : {1} : {2}", result.RspCode, result.Message, ex.Message.ToString());

        }
        return result;
    }

}
