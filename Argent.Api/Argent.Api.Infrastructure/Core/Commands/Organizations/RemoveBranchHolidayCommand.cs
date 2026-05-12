using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public record RemoveBranchHolidayCommand(long HolidayId)
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "RemoveBranchHoliday";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "BranchHoliday";
    }
}
