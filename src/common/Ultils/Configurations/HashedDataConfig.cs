namespace Ultils.Configurations;
public class HashedDataConfig
{
    public const string ConfigName = "HashedData";
    public string? AesKey { get; set; }
    public string? AesIV { get; set; }
    public int? RsaKeySize { get; set; }
    public string? RsaSenderPrivateKey { get; set; }
    public string? RsaSenderPublicKey { get; set; }
}

