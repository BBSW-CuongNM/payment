using Logic.Commands;
using MoMo.Config;
using MoMo.Helpers;

namespace Logic.CommandHandlers;
public class MoMoPaymentRerturnCommandHandler
    : IRequestHandler<MoMoPaymentReturnCommand, CommonCommandResult>
{
    private readonly ApplicationContext applicationContext;
    private readonly IMapper mapper;
    private readonly ILogger<MoMoPaymentRerturnCommandHandler> logger;
    private readonly IPaymentProcess paymentProcess;
    private readonly IMediator mediator;
    private readonly MoMoConfig moMoconfig;
    private readonly HashedDataConfig hashedDataConfig;
    private readonly MoMoConfig momoConfig;

    public MoMoPaymentRerturnCommandHandler(ApplicationContext applicationContext,
        IMapper mapper,
        IOptions<MoMoConfig> momoConfig,
        IOptions<HashedDataConfig> hashedDataConfig,
        ILogger<MoMoPaymentRerturnCommandHandler> logger,
        IPaymentProcess paymentProcess,
        IOptions<MoMoConfig> momoconfig,
        IMediator mediator)
    {
        this.applicationContext = applicationContext;
        this.mapper = mapper;
        this.logger = logger;
        this.paymentProcess = paymentProcess;
        this.mediator = mediator;
        this.moMoconfig = momoconfig.Value;
        this.hashedDataConfig = hashedDataConfig.Value;
        this.momoConfig = momoConfig.Value;
    }

    public async Task<CommonCommandResult> Handle(MoMoPaymentReturnCommand request,
        CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var payment = applicationContext.Payments.FirstOrDefault(x => x.Id == request.RequestId);
            var orderInfo = await applicationContext.Orders.FirstOrDefaultAsync(x => x.ServiceCode == payment.ServiceCode
                                                                 && x.ServiceOrderId == payment.ServiceOrderId
                                                                 && x.IsDeleted == false
                                                                 && x.Status != Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.PAID));
            if (payment != null)
            {
                /// TODO: check signature
                /// 
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
                if (signatureResponse.Equals(request.Signature))
                {
                    string paymentStatus = PaymentStatusConst.FAILURE;
                    string orderStatus = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.WAIT) ?? string.Empty;
                    if (request.ResultCode == 0)
                    {
                        payment.Status = Enum.GetName(typeof(PaymentStatusEnum), PaymentStatusEnum.COMPLETED) ?? String.Empty;
                        paymentStatus = PaymentStatusConst.SUCCESS;
                        orderStatus = Enum.GetName(typeof(OrderStatusEnum), OrderStatusEnum.PAID) ?? string.Empty;
                    }
                    else
                    {
                        payment.Status = Enum.GetName(typeof(PaymentStatusEnum), PaymentStatusEnum.FAILURE) ?? String.Empty;
                    }
                    payment.PaymentReferenceCode = request.TransId.ToString();
                    applicationContext.Payments.Update(payment);
                    if (orderInfo != null)
                    {
                        orderInfo.Status = orderStatus;
                        applicationContext.Orders.Update(orderInfo);

                    }
                    var comit = applicationContext.SaveChanges();
                    // gọi service code
                    if (comit < 0)
                    {
                        logger.LogError("---Save failure : Order: {@OrderId} :  {@ReferenceCode}", payment.OrderId, payment.PaymentReferenceCode);

                    }
                    else
                    {
                        DateTime dateTime = DateTime.MinValue;
                        await mediator.Publish(new PaymentCallBackEvent
                        {
                            ServiceCode = payment.ServiceCode,
                            Amount = request.Amount,
                            PaymentType = request.PayType,
                            TransactionStatus = paymentStatus,
                            TransactionNumber = request.TransId.ToString(),
                            TransactionDate = dateTime.AddMilliseconds(request.ResponseTime).ToLocalTime(),
                            OrderId = payment.ServiceOrderId,
                            ReferenceCode = payment.ServiceReferenceCode,
                            IPNUrl = payment.IPNUrl
                        });
                    }
                }
                else
                {
                    logger.LogError("---Invalid signature : Order: {@OrderId} :  {@ReferenceCode}", payment.OrderId, payment.PaymentReferenceCode);

                    /// TODO: log invalid signature
                }

            }
            else
            {
                logger.LogError("---Invalid payment : Order: {@OrderId} :  {@TransId}", request.OrderId, request.TransId.ToString());
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            logger.LogError("-- Exception: ", ex.Message.ToString());

        }

        return result;
    }
}
