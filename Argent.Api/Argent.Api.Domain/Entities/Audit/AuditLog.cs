using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Audit {

    /// <summary>
    /// Class used to be written automatically by AuditPipelineBehavior for every MediatR command.
    /// </summary>
    public class AuditLog : BaseEntity {
        public long? UserId { get; set; }
        public string? Username { get; set; }
        public long? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? IpAddress { get; set; }
        public string Module { get; set; } = string.Empty;      
        public string Action { get; set; } = string.Empty;       
        public string? EntityName { get; set; }                  
        public string? EntityId { get; set; }                    
        public AuditAction ActionType { get; set; }
        public string? OldValues { get; set; }  
        public string? NewValues { get; set; } 
        public bool Succeeded { get; set; }
        public string? FailureReason { get; set; }
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
        public long DurationMs { get; set; }
    }
}
