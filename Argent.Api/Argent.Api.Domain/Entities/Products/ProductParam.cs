using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Stores typed product configuration as key-value pairs.
    /// Seeded automatically from the [ConfigParam]-annotated properties on each
    /// product's configuration class when the product is created.
    ///
    /// Allows UI-driven configuration without schema changes.
    /// Strongly-typed configuration is accessed via the ProductConfiguration entity.
    /// Both are kept in sync — the ProductConfiguration entity is the authoritative source,
    /// ProductParam is for UI display and extensibility.
    /// </summary>
    public class ProductParam : BaseEntity {
        public long ProductId { get; set; }
        public ProductModuleType ProductModule { get; set; }

        /// <summary>Parameter name — matches the [ConfigParam] Name attribute.</summary>
        public string ParameterName { get; set; } = string.Empty;

        /// <summary>Current value as string — parsed by DataType at read time.</summary>
        public string ParamValue { get; set; } = string.Empty;

        /// <summary>Expected .NET type: "string", "bool", "int", "decimal", "datetime", "array".</summary>
        public string DataType { get; set; } = "string";

        public string? Description { get; set; }
        public bool IsEditable { get; set; } = true;
    }

}
