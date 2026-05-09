namespace Argent.Api.Infrastructure.Data.Security {
    public interface IEncryptionService {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}
