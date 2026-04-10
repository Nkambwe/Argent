using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Common.Interfaces {
    /// <summary>
    /// Marker interface. Commands that implement this tell AuditPipelineBehavior
    /// which module, action type, and entity name to record in the AuditLog.
    ///
    /// Every command that modifies data should implement this.
    /// Read-only queries typically don't need to — the pipeline skips them.
    /// </summary>
    public interface IAuditableCommand {
        string AuditModule { get; }         
        string AuditAction { get; }         
        AuditAction AuditActionType { get; }
        string? AuditEntityName { get; }    
    }
}
