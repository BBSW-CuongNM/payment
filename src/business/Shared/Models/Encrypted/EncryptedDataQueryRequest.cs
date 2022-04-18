namespace Shared.Models;
public class EncryptedDataQueryRequest
{
    public string? RsaSenderPrivateKey { get; set; }
    public string? RsaReceiverPublicKey { get; set; }
    public string? AesKey { get; set; }
    public string? AesIV { get; set; }
    public string? TextToEncrypt { get; set; }
}

