using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    public class IdentificationType : BaseEntity {
        /// <summary>
        /// National ID, Passport, Driving Permit, etc. 
        /// </summary>
        public string TypeName { get; set; } = string.Empty;  

        /// <summary>
        /// Must this document always be captured?
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Can this document alone satisfy the ID requirement without additional documents?
        /// </summary>
        public bool Sufficient { get; set; }

        public Priority Priority { get; set; }
        public string? LocalFolder { get; set; }
        public string? FtpFolder { get; set; }
        public string? Notes { get; set; }
    }
}
