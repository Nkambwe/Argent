using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public record AddBranchHolidayCommand(long BranchId, AddHolidayRequest Request)
        : IRequest<Result<BranchHolidayDto>>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "AddBranchHoliday";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "BranchHoliday";
    }
}
