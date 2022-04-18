namespace Data.Entities;
public class User : IdentityUser
{
    public User() { }
    public User(string userName, string partnerId, string partnerKey, string publicKey, string privateKey)
    {
        Id = Guid.NewGuid().ToString();
        UserName = userName;
        PartnerId = partnerId;
        PublicKey = publicKey;
        PrivateKey = privateKey;
        PartnerKey = partnerKey;
    }
    public string? PartnerKey { get; set; }
    public string? PartnerId { get; set; }
    public string? PublicKey { get; set; }
    public string? PrivateKey { get; set; }
    public string? RefreshToken { get; set; }
    public bool Enable { get; set; }
}
