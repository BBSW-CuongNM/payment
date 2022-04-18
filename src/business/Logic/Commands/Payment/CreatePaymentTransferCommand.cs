using Shared.Models;

namespace Logic.Commands;
public class CreatePaymentTransferCommand : IRequest<CommonCommandResultHasData<PaymentResponeModel>>
{
    public PaymentModel? Data { get; set; }
    public string HashedData { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
}

