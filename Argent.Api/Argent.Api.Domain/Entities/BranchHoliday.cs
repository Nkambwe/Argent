using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities {
    public class BranchHoliday : BaseEntity {
        public long BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        /// <summary>
        /// Holiday name e.g. "Christmas Day", "Eid al-Fitr"
        /// </summary>
        public string Name { get; set; } = string.Empty;       
        public DateOnly HolidayDate { get; set; }
        public HolidayType Type { get; set; }
        public HolidayRecurrence Recurrence { get; set; }

        /// <summary>
        /// If Recurrence = Annual, only Month+Day are used for matching — year is ignored.
        /// </summary>
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
    }
}
