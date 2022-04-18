using System.Net;

namespace VNPay.Helpers;
public class VNPaySecurityHelper
{
    //public static bool ValidateSignature(SortedList<string, string> keyValuePairs, string inputHash, string secretKey)
    //{
    //    StringBuilder data = new StringBuilder();
    //    if (keyValuePairs.ContainsKey("vnp_SecureHashType"))
    //    {
    //        keyValuePairs.Remove("vnp_SecureHashType");
    //    }
    //    if (keyValuePairs.ContainsKey("vnp_SecureHash"))
    //    {
    //        keyValuePairs.Remove("vnp_SecureHash");
    //    }
    //    foreach (KeyValuePair<string, string> kv in keyValuePairs)
    //    {
    //        if (!String.IsNullOrEmpty(kv.Value))
    //        {
    //            data.Append(kv.Key + "=" + kv.Value + "&");
    //        }
    //    }
    //    if (data.Length > 0)
    //    {
    //        data.Remove(data.Length - 1, 1);
    //    }

    //    var rawData = data.ToString();
    //    string checkSum = HmacSHA512(secretKey, rawData);
    //    return checkSum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    //}
    public static bool ValidateSignature(SortedList<String, String> responeData,string inputHash, string secretKey)
    {
        string rspRaw = GetResponseData(responeData);
        string myChecksum = HmacSHA512(secretKey, rspRaw);
        return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }
    public static string GetResponseData(SortedList<String, String> responeData)
    {

        StringBuilder data = new StringBuilder();
        if (responeData.ContainsKey("vnp_SecureHashType"))
        {
            responeData.Remove("vnp_SecureHashType");
        }
        if (responeData.ContainsKey("vnp_SecureHash"))
        {
            responeData.Remove("vnp_SecureHash");
        }
        foreach (KeyValuePair<string, string> kv in responeData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }
        //remove last '&'
        if (data.Length > 0)
        {
            data.Remove(data.Length - 1, 1);
        }
        return data.ToString();
    }
    public static string Md5(string sInput)
    {
        HashAlgorithm algorithmType = default(HashAlgorithm)!;
        ASCIIEncoding enCoder = new ASCIIEncoding();
        byte[] valueByteArr = enCoder.GetBytes(sInput);
        byte[] hashArray = null;
        algorithmType = new MD5CryptoServiceProvider();
        hashArray = algorithmType.ComputeHash(valueByteArr);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashArray)
        {
            sb.AppendFormat("{0:x2}", b);
        }
        return sb.ToString();
    }
    public static string HmacSHA512(string key, string data)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (var sha256Hash = new HMACSHA512(keyBytes))
        {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }
    public static string Encrypt(string code, string input)
    {
        using (var md5 = new MD5CryptoServiceProvider())
        {
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(code));
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (var transform = tdes.CreateEncryptor())
                {
                    byte[] textBytes = UTF8Encoding.UTF8.GetBytes(input);
                    byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    return Convert.ToBase64String(bytes, 0, bytes.Length);
                }
            }
        }
    }
    public static string Decrypt(string code, string input)
    {
        using (var md5 = new MD5CryptoServiceProvider())
        {
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(code));
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                using (var transform = tdes.CreateDecryptor())
                {
                    byte[] cipherBytes = Convert.FromBase64String(input);
                    byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return UTF8Encoding.UTF8.GetString(bytes);
                }
            }
        }
    }
}
