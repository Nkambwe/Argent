using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record UpdateSavingProductCommand(long Id, UpdateSavingProductRequest Request)
        : IRequest<Result<SavingProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "UpdateSavingProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "SavingProduct";
    }
}
