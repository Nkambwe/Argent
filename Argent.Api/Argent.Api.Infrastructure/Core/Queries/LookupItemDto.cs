namespace Argent.Api.Infrastructure.Core.Queries {
    public class LookupItemDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
    }

}
