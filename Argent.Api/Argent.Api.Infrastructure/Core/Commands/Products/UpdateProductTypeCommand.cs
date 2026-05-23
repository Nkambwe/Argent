using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record UpdateProductTypeCommand(long Id, UpdateProductTypeRequest Request) : IRequest<Result<ProductTypeDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "UpdateProductType";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "ProductType";
    }
}
