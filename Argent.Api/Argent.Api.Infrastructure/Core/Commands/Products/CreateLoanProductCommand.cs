using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record CreateLoanProductCommand(CreateLoanProductRequest Request)
    : IRequest<Result<LoanProductDetailDto>>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "CreateLoanProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "LoanProduct";
    }
}
