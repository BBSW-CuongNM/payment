
using Abstraction;
using Abstraction.Model;
using MoMo.Config;
using Shared.Config;
using Shared.Models;
using Ultils.Helpers;
using VNPay.Config;
using ZaloPay.Config;

namespace Logic.CommandHandlers;
public class CreatePaymentTransferCommandHandler : IRequestHandler<CreatePaymentTransferCommand, CommonCommandResultHasData<PaymentResponeModel>>
{
    private readonly ILogger<CreatePaymentTransferCommandHandler> logger;
    private readonly ApplicationContext applicationContext;
    private readonly PaymentConfig paymentConfig;
    private readonly VNPayConfig vnpayConfig;
    private readonly MoMoConfig momoConfig;
    private readonly ZaloConfig zaloConfig;
    private readonly IPaymentService paymentService;
    private readonly IMediator mediator;
    private readonly ErrorConfig errorConfig;
    private readonly HashedDataConfig hashedDataConfig;

    public CreatePaymentTransferCommandHandler(ILogger<CreatePaymentTransferCommandHandler> logger,
             ApplicationContext applicationContext,
             IOptions<HashedDataConfig> hashedDataConfig,
             IOptions<ErrorConfig> errorConfig,
             IOptions<VNPayConfig> vnpayConfig,
             IOptions<MoMoConfig> momoConfig,
             IOptions<ZaloConfig> zaloConfig,
             IOptions<PaymentConfig> paymentConfig,
             IPaymentService paymentService,
             IMediator mediator)
    {
        this.logger = logger;
        this.applicationContext = applicationContext;
        this.paymentConfig = paymentConfig.Value;
        this.vnpayConfig = vnpayConfig.Value;
        this.momoConfig = momoConfig.Value;
        this.zaloConfig = zaloConfig.Value;
        this.paymentService = paymentService;
        this.mediator = mediator;
        this.errorConfig = errorConfig.Value;
        this.hashedDataConfig = hashedDataConfig.Value;
    }

    public async Task<CommonCommandResultHasData<PaymentResponeModel>> Handle(CreatePaymentTransferCommand request,
        CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<PaymentResponeModel>();

        try
        {
            var partner = await applicationContext.Partners.FirstOrDefaultAsync(x => x.Id == request.Data!.ServiceCode);

            var verify = true;// SecurityHelper.VerifyHash(partner?.PartnerPublicKey ?? string.Empty, Convert.FromBase64String(request?.HashedData ?? string.Empty), Convert.FromBase64String(request?.Signature ?? string.Empty));

            if (verify)
            {
                var paymentId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);

                // create order
                var orderId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
                var order = await mediator.Send(new CreateOrderTransferCommand
                {
                    Id  = orderId,
                    ServiceCode = request!.Data!.ServiceCode,
                    OrderId = request.Data?.OrderId,
                    OrderDate = DateTime.Now,
                    Amount = request!.Data!.Amount,
                    IsInstallmentAllowed =false,
                    Note = request.Data.Description??string.Empty

                });
                if (!order.Success)
                {
                    result.SetResult(false, errorConfig.GetByKey("SaveOrderError"));
                    return result;
                }
                if (request.Data.Amount < 5000)
                {
                    result.SetResult(false, errorConfig.GetByKey("InvalidAmount"));
                    return result;
                }
                if (string.IsNullOrEmpty(request.Data.CurrencyUnit))
                    request.Data.CurrencyUnit = "VND";
                if (string.IsNullOrEmpty(request.Data.Description))
                    request.Data.Description = string.Format("{0} : {1}", "Thanh toán đơn hàng :", request.Data.OrderId);
                var payment = new Payment
                {
                    Id = paymentId,
                    Command = request.Data.Command,
                    ServiceReferenceCode = request.Data.ReferenceCode,
                    PaymentReferenceCode = string.Empty,
                    OrderId = orderId,
                    ServiceCode = request.Data.ServiceCode,
                    ServiceOrderId = request.Data.OrderId,
                    Amount = request.Data.Amount,
                    CurrencyUnit = request.Data.CurrencyUnit,
                    Via = request.Data.PaymentVia,
                    Destination = request.Data.PaymentDestinationId,
                    Language = request.Data.Language,
                    Description = request.Data.Description ?? string.Empty,
                    ReturnUrl = request.Data.ReturnUrl,
                    IPNUrl = request.Data.IPNUrl,
                    Status = string.Empty,
                    ErrorMessage = string.Empty,
                };

                await applicationContext.Payments.AddAsync(payment);
                var comit = await applicationContext.SaveChangesAsync();

                if (comit < 0)
                {
                    result.SetResult(false, errorConfig.GetByKey("SaveOrderError"));
                    return result;
                }
                else
                {
                    var paymentDestination = applicationContext.PaymentDestionations
                        .FirstOrDefault(x => x.Id == payment.Destination);

                    var paymentRequestModel = new PaymentRequestModel()
                        .SetFromPayment(payment, vnpayConfig, momoConfig, zaloConfig);

                    var paymentRedirectResult = paymentService
                        .GetPayment(paymentRequestModel, payment.Via.ToUpper());

                    if (paymentRedirectResult.IsSuccess)
                    {
                        result.SetData(new PaymentResponeModel
                        {
                            PaymentUrl = paymentRedirectResult.RedirectUrl,
                            PaymentTimeoutAfterMinutes = 0
                        }).SetResult(true, "");
                    }
                    else
                    {
                        result.SetResult(false, paymentRedirectResult.Message);
                    }
                }

            }
            else
            {
                result.SetResult(false, errorConfig.GetByKey("InvalidSignature"));
            }
            return result;
        }
        catch (Exception ex)
        {
            result.SetResult(false, ex.Message.ToString());
            return result;
        }
    }
}
