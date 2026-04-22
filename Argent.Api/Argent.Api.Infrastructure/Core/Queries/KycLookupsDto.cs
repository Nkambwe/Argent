namespace Argent.Api.Infrastructure.Core.Queries {
    public class KycLookupsDto {
        public IEnumerable<LookupItemDto> Titles { get; set; } = [];
        public IEnumerable<LookupItemDto> Nationalities { get; set; } = [];
        public IEnumerable<LookupItemDto> Professions { get; set; } = [];
        public IEnumerable<LookupItemDto> EducationLevels { get; set; } = [];
        public IEnumerable<LookupItemDto> IncomeTypes { get; set; } = [];
        public IEnumerable<LookupItemDto> GroupPositions { get; set; } = [];
        public IEnumerable<LookupItemDto> IdentificationTypes { get; set; } = [];
        public IEnumerable<LookupItemDto> IssuerAuthorities { get; set; } = [];
    }

}
