namespace VNPay.Config;
public class VNPayConfig
{
    public static string ConfigName { get; set; } = "VNPay";
    public string Host { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string PayCommand { get; set; } = string.Empty;
    public string RefundCommand { get; set; } = string.Empty;
    public string TMNCode { get; set; } = string.Empty;
    public string HashSecret { get; set; } = string.Empty;
    public string OrderType { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
}
