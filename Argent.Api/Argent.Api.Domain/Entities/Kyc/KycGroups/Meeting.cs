using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// A group meeting record.
    /// </summary>
    public class Meeting : BaseEntity {
        public long GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public DateTime MeetingDate { get; set; }
        public string? Venue { get; set; }
        public string? Agenda { get; set; }
        public string? Minutes { get; set; }
        public string? Notes { get; set; }
    }
}
