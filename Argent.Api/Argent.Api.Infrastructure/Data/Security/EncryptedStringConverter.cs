using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Argent.Api.Infrastructure.Data.Security {
    public class EncryptedStringConverter(IEncryptionService encryption)
        : ValueConverter<string, string>(
            v => encryption.Encrypt(v),      
            v => encryption.Decrypt(v)) {
    }
}
