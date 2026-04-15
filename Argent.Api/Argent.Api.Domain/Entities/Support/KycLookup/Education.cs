using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {

    public class Education : BaseEntity {
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Primary, Secondary, Degree, etc.
        /// </summary>
        public string Name { get; set; } = string.Empty;   
    }
}
