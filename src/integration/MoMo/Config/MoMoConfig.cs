namespace MoMo.Config;
public class MoMoConfig
{
    public static string ConfigName { get; set; } = "MoMo";
    public string PartnerCode { get; set; } = string.Empty;
    public string PartnerName { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string APIEndPoint { get; set; } = string.Empty;
    public string IPNUrl { get; set; } = string.Empty;
    public string DefaultRequestType { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
}
