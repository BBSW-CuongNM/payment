using Shared.Models;

namespace Logic.Commands;
public class CreatePaymentCommand : IRequest<CommonCommandResultHasData<PaymentResponeModel>>
{
    public PaymentModel? Data { get; set; }
    public string HashedData { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
}

