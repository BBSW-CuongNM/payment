namespace Logic.Commands;
public class DeleteOrderCommand : IRequest<CommonCommandResult>
{
    public DeleteOrderData? Data { get; set; }
    public string? HashedData { get; set; }
    public string? Signature { get; set; }
}
public class DeleteOrderData
{
    /// <summary>
    /// Mã order service payment cấp cho đối tác
    /// </summary>
    public string ServiceCode { get; set; } = string.Empty;

    /// <summary>
    /// Mã đơn hàng của đối tác
    /// </summary>
    public string ServiceOrderId { get; set; } = string.Empty;
}

