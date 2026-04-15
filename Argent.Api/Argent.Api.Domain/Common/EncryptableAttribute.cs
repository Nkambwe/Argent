namespace Argent.Api.Domain.Common {
    /// <summary>
    /// Marks a string property as containing PII that should be encrypted at rest.
    /// EF Core value converters will encrypt on write and decrypt on read.
    /// The DisplayName is used in audit logs and UI labels.
    ///
    /// Usage: [Encryptable("First Name")]
    /// public string FirstName { get; set; }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptableAttribute(string displayName) : Attribute {
        public string DisplayName { get; } = displayName;
    }
}
