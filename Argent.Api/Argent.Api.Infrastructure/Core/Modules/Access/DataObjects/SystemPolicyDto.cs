namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class SystemPolicyDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultValue { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public bool IsOverridable { get; set; }
    }

}
