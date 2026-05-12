using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects {
    public class BranchHolidayDto {
        public long Id { get; set; }
        public long BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public HolidayRecurrence Recurrence { get; set; }
        public bool IsActive { get; set; } = true;
        public string Notes { get; set; } = string.Empty;
    }
}
