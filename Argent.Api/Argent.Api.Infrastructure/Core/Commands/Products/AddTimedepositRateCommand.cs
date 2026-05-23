using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record AddTimedepositRateCommand(long ProductId, CreateTimedepositRateRequest Request)
        : IRequest<Result<TimedepositRateDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "AddTimedepositRate";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "TimedepositRate";
    }
}
