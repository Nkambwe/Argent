using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record DeleteLoanProductCommand(long Id) : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Products";
        public string AuditAction => "DeleteLoanProduct";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "LoanProduct";
    }
}
