namespace Ultils.Helpers;

public class SecurityHelper
{
    public static string SHA256(string data, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(data);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            string sbinary = "";
            for (int i = 0; i < hashmessage.Length; i++)
            {
                sbinary += hashmessage[i].ToString("x2");
            }
            return sbinary;
        }
    }

    public static string MD5(string input)
    {
        String str = "";
        Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        buffer = md5.ComputeHash(buffer);
        foreach (Byte b in buffer)
        {
            str += b.ToString("X2");
        }
        return str;
    }

    public static string SHA256(string data)
    {
        using (var sha256Hash = System.Security.Cryptography.SHA256.Create())
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


    //Manually performs hash and then signs hashed value.
    public static byte[] HashAndSign(string rsaSenderPrivateParams, byte[] encrypted, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);
        SHA1Managed hash = new();
        byte[] hashedData;

        rsaCSP.FromXmlString(rsaSenderPrivateParams);

        hashedData = hash.ComputeHash(encrypted);
        return rsaCSP.SignHash(hashedData, CryptoConfig.MapNameToOID("SHA1"));
    }

    public static byte[] HashAndSign(RSAParameters rsaSenderPrivateParams, byte[] encrypted, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);
        SHA1Managed hash = new();
        byte[] hashedData;

        rsaCSP.ImportParameters(rsaSenderPrivateParams);

        hashedData = hash.ComputeHash(encrypted);
        return rsaCSP.SignHash(hashedData, CryptoConfig.MapNameToOID("SHA1"));
    }

    //Encrypts using only the public key data.
    public static byte[] EncryptData(RSAParameters rsaRecieverPublicParams, byte[] toEncrypt, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);

        rsaCSP.ImportParameters(rsaRecieverPublicParams);
        return rsaCSP.Encrypt(toEncrypt, false);
    }

    public static byte[] EncryptData(string rsaRecieverPublicParams, byte[] toEncrypt, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);

        rsaCSP.FromXmlString(rsaRecieverPublicParams);
        return rsaCSP.Encrypt(toEncrypt, false);
    }

    //Manually performs hash and then verifies hashed value.
    public static bool VerifyHash(RSAParameters rsaSenderPublicParams, byte[] signedData, byte[] signature, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);
        SHA1Managed hash = new();
        byte[] hashedData;

        rsaCSP.ImportParameters(rsaSenderPublicParams);
        bool dataOK = rsaCSP.VerifyData(signedData, CryptoConfig.MapNameToOID("SHA1"), signature);
        hashedData = hash.ComputeHash(signedData);
        return rsaCSP.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), signature);
    }

    public static bool VerifyHash(string rsaSenderPublicParams, byte[] signedData, byte[] signature, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);
        SHA1Managed hash = new SHA1Managed();
        byte[] hashedData;

        rsaCSP.FromXmlString(rsaSenderPublicParams);
        bool dataOK = rsaCSP.VerifyData(signedData, CryptoConfig.MapNameToOID("SHA1"), signature);
        hashedData = hash.ComputeHash(signedData);
        return rsaCSP.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), signature);
    }

    //Decrypt using the private key data.
    public static byte[] DecryptRsa(string rsaReceiverPrivateParams, byte[] encrypted, int rsaKeySize = 1024)
    {

        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);

        rsaCSP.FromXmlString(rsaReceiverPrivateParams);
        return rsaCSP.Decrypt(encrypted, false);
    }

    public static byte[] DecryptRsa(RSAParameters rsaReceiverPrivateParams, byte[] encrypted, int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);

        rsaCSP.ImportParameters(rsaReceiverPrivateParams);
        return rsaCSP.Decrypt(encrypted, false);
    }

    /// <summary>
    /// Generate RSA pair key in Xml format
    /// </summary>
    /// <returns>Pair(Public Key, Private Key)</returns>
    public static (string PublicKey, string PrivateKey) GenerateRSAXml(int rsaKeySize = 1024)
    {
        RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(rsaKeySize);

        return (rsaCSP.ToXmlString(false), rsaCSP.ToXmlString(true));
    }

    public static byte[] EncryptAes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptAes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
