namespace Ultils.Configurations;
public class JwtConfig
{
    public const string ConfigName = "Jwt";
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public int? JwtTokenTimeOut { get; set; }
}

