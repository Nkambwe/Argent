using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record UpdateSavingProductConfigCommand(long Id, UpdateSavingProductConfigRequest Request)
    : IRequest<Result<SavingProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "UpdateSavingProductConfig";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "SavingProductConfiguration";
    }
}
