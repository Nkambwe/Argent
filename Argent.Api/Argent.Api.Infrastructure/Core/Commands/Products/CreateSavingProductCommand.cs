using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record CreateSavingProductCommand(CreateSavingProductRequest Request)
    : IRequest<Result<SavingProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "CreateSavingProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "SavingProduct";
    }
}
