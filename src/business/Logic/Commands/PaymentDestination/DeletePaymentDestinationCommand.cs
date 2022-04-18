namespace Logic.Commands;
public class DeletePaymentDestinationCommand : IRequest<CommonCommandResult>
{
    /// <summary>
    /// Mã đích thanh toán
    /// VD: VNPAY...
    /// </summary>
    public string Id { get; set; } = string.Empty;
}

