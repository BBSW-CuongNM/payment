using MoMo.Model;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace MoMo.Helpers;
public static class MoMoSecurityHelper
{
    public static string SignSHA256(this string message, string key)
    {
        byte[] keyByte = Encoding.UTF8.GetBytes(key);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            string hex = BitConverter.ToString(hashmessage);
            hex = hex.Replace("-", "").ToLower();
            return hex;
        }
    }

    public static string ToQueryHash(MoMoHashModel model)
    {
        string result = string.Empty;
        var json = JsonConvert.SerializeObject(model);

        byte[] data = Encoding.UTF8.GetBytes(json);
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                rsa.FromXmlString(model.PublicKey);
                var encryptedData = rsa.Encrypt(data, false);
                var base64Encrypted = Convert.ToBase64String(encryptedData);
                result = base64Encrypted;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        return result;
    }
}
