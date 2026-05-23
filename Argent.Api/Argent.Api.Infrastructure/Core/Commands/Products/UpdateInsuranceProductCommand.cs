using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record UpdateInsuranceProductCommand(long Id, UpdateInsuranceProductRequest Request)
        : IRequest<Result<InsuranceProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "UpdateInsuranceProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "InsuranceProduct";
    }

}
