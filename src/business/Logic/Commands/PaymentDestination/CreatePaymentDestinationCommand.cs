﻿namespace Logic.Commands;

public class CreatePaymentDestinationCommand : IRequest<CommonCommandResultHasData<PaymentDestination>>
{
    public string? ExternalId { get; set; }
    /// <summary>
    /// Group các đích thanh toán : 
    /// </summary>
    public string Group { get; set; } = string.Empty;
    /// <summary>
    /// Parent group
    /// VD: VNPAY => có các đích thanh toán con...:   Ví điện tử (momo, vnpay, ...), visa, ,master card, 
    /// </summary>
    public string ParentId { get; set; } = string.Empty;
    /// <summary>
    /// Tên gọi đích thanh toán
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Tên gọi tắt, ACB
    /// </summary>
    public string OtherName { get; set; } = string.Empty;
    /// <summary>
    /// Image url
    /// </summary>
    public string Image { get; set; } = string.Empty;
    /// <summary>
    /// Thứ tự sắ xếp
    /// </summary>
    public int SortIndex { get; set; }
    /// <summary>
    /// partnerID : VNpay, Momo,...
    /// </summary>
    public string? PartnerId { get; set; }
}



