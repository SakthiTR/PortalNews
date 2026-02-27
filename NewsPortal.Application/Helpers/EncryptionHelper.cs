using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NewsPortal.Application.Helpers
{
    public class EncryptionHelper
    {
        private readonly IConfiguration _configuration;
        
        public EncryptionHelper(IConfiguration configuration) 
        {
            _configuration = configuration;            
        }
        public string Encrypt(string plainText)
        {
            var Encryptions = _configuration.GetSection("Encryption");
            var EncryptionKey = Encryptions["Key"];
            var EncryptionIV = Encryptions["IV"];
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(EncryptionIV);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            var Encryptions = _configuration.GetSection("Encryption");
            var EncryptionKey = Encryptions["Key"];
            var EncryptionIV = Encryptions["IV"];
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = Encoding.UTF8.GetBytes(EncryptionIV);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
