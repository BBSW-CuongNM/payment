
namespace Shared.Models;

public class EncryptedDataResponse
{
    public string? RsaEncryptedAesKey { get; set; }
    public string? RsaSignature { get; set; }
    public string? AesEncryptedData { get; set; }
}

