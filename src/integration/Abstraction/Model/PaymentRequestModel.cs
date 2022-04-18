using Data.Entities;
using MoMo.Config;
using MoMo.Request;
using VNPay.Config;
using VNPay.Request;
using ZaloPay.Config;
using ZaloPay.Request;

namespace Abstraction.Model;
public class PaymentRequestModel
{
    /// <summary>
    /// Momo
    /// </summary>
    public string PartnerCode { get; set; } = string.Empty;
    public string PartnerName { get; set; } = string.Empty;
    public string StoreId { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
    public long Amount { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string OrderInfo { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string IPNUrl { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public string ExtraData { get; set; } = string.Empty;
    public bool AutoCapture { get; set; }
    public string Lang { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;

    /// <summary>
    /// VNPay
    /// </summary>
    public string VNP_Version { get; set; } = string.Empty;
    public string VNP_Command { get; set; } = string.Empty;
    public string VNP_TmnCode { get; set; } = string.Empty;
    public string VNP_Locale { get; set; } = string.Empty;
    public string VNP_CurrCode { get; set; } = string.Empty;
    public string VNP_TxnRef { get; set; } = string.Empty;
    public string VNP_OrderInfo { get; set; } = string.Empty;
    public string VNP_OrderType { get; set; } = string.Empty;
    public decimal VNP_Amount { get; set; }
    public string VNP_ReturnUrl { get; set; } = string.Empty;
    public string VNP_IpAddr { get; set; } = string.Empty;
    public DateTime VNP_CreateDate { get; set; }
    public string VNP_BankCode { get; set; } = string.Empty;

    /// <summary>
    /// Zalo pay
    /// </summary>
    public int AppId { get; set; }
    public string AppUser { get; set; } = string.Empty;
    public long AppTime { get; set; }
    public string AppTransId { get; set; } = string.Empty;
    public string EmbedData { get; set; } = string.Empty;
    public string Item { get; set; } = string.Empty;
    public string Mac { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string SubAppId { get; set; } = string.Empty;

    public MoMoPaymentRequest ToMoMoPaymentRequest()
    {
        return new MoMoPaymentRequest()
        {
            PartnerCode = this.PartnerCode,
            PartnerName = this.PartnerName,
            StoreId = this.StoreId,
            RequestId = this.RequestId,
            Amount = this.Amount,
            OrderId = this.OrderId,
            OrderInfo = this.OrderInfo,
            RedirectUrl = this.RedirectUrl,
            IPNUrl = this.IPNUrl,
            RequestType = this.RequestType,
            ExtraData = this.ExtraData,
            AutoCapture = this.AutoCapture,
            Lang = this.Lang,
            Signature = this.Signature,
        };
    }
    public VNPayPaymentRequest ToVNPayPaymentRequest()
    {
        return new VNPayPaymentRequest()
        {
            VNP_Version = this.VNP_Version,
            VNP_Command = this.VNP_Command,
            VNP_TmnCode = this.VNP_TmnCode,
            VNP_Locale = this.VNP_Locale,
            VNP_CurrCode = this.VNP_CurrCode,
            VNP_TxnRef = this.VNP_TxnRef,
            VNP_OrderInfo = this.VNP_OrderInfo,
            VNP_OrderType = this.VNP_OrderType,
            VNP_Amount = this.VNP_Amount,
            VNP_ReturnUrl = this.VNP_ReturnUrl,
            VNP_IpAddr = this.VNP_IpAddr,
            VNP_CreateDate = this.VNP_CreateDate,
            VNP_BankCode = this.VNP_BankCode,
        };
    }
    public ZaloPaymentRequest ToZaloPayPaymentRequest()
    {
        return new ZaloPaymentRequest()
        {

        };
    }

    public PaymentRequestModel SetFromPayment(Payment payment,
        VNPayConfig vnpayConfig, MoMoConfig moMoConfig, ZaloConfig zaloConfig)
    {
        /// Vnpay
        this.VNP_Version = vnpayConfig.Version;
        this.VNP_Command = payment.Command;
        this.VNP_TmnCode = vnpayConfig.TMNCode;
        this.VNP_Locale = "vn";
        this.VNP_CurrCode = payment.CurrencyUnit ?? "VND";
        this.VNP_TxnRef = payment.Id;
        this.VNP_OrderInfo = payment.Description;
        this.VNP_OrderType = string.Empty;
        this.VNP_Amount = payment.Amount;
        this.VNP_ReturnUrl = vnpayConfig.ReturnUrl;
        this.VNP_IpAddr = string.Empty;
        this.VNP_CreateDate = payment.CreatedAt ?? DateTime.Now;
        this.VNP_BankCode = payment.Destination ?? string.Empty;

        /// Momo
        this.PartnerCode = moMoConfig.PartnerCode;
        this.PartnerName = moMoConfig.PartnerName;
        this.StoreId = string.Empty;
        this.RequestId = payment.Id;
        this.Amount = (long)payment.Amount;
        this.OrderId = payment.Id;
        this.OrderInfo = payment.Description ?? string.Empty;
        this.RedirectUrl = moMoConfig.ReturnUrl;
        this.IPNUrl = moMoConfig.IPNUrl;
        this.RequestType = moMoConfig.DefaultRequestType;
        this.ExtraData = string.Empty;
        this.AutoCapture = true;
        this.Lang ="vi";

        return this;
    }
}
