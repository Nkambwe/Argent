using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A folio type groups related transaction description codes.
    /// e.g. FolioType "Fixed Assets" contains Folios like "Land", "Buildings", "Equipment"
    /// </summary>
    public class FolioType : BaseEntity {
        /// <summary>Short code — e.g. "FIXASS" for fixed assets.</summary>
        public string Code { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;

        public ICollection<Folio> Folios { get; set; } = [];
    }
}
