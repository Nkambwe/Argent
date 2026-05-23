using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record UpdateShareProductCommand(long Id, UpdateShareProductRequest Request)
        : IRequest<Result<ShareProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products"; 
        public string AuditAction => "UpdateShareProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update; 
        public string? AuditEntityName => "ShareProduct";
    }

}
