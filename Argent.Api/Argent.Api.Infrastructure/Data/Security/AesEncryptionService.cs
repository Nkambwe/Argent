using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Argent.Api.Infrastructure.Data.Security {
    public class AesEncryptionService : IEncryptionService {
        private readonly byte[] _key;

        public AesEncryptionService(IConfiguration configuration) {
            var keyBase64 = configuration["Encryption:Key"]
                ?? throw new InvalidOperationException("Encryption:Key not configured.");
            _key = Convert.FromBase64String(keyBase64);
        }

        public string Encrypt(string plaintext) {
            if (string.IsNullOrEmpty(plaintext)) return plaintext;
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            using var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            // Prepend IV so we can decrypt later
            var result = new byte[aes.IV.Length + encrypted.Length];
            aes.IV.CopyTo(result, 0);
            encrypted.CopyTo(result, aes.IV.Length);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string ciphertext) {
            if (string.IsNullOrEmpty(ciphertext)) return ciphertext;
            var buffer = Convert.FromBase64String(ciphertext);
            using var aes = Aes.Create();
            aes.Key = _key;
            var iv = buffer[..16];
            var data = buffer[16..];
            aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            var decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
