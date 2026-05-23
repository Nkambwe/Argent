namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class ProductParamDto {
        public long Id { get; set; }
        public string ParameterName { get; set; } = string.Empty;
        public string ParamValue { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsEditable { get; set; }
    }

}
