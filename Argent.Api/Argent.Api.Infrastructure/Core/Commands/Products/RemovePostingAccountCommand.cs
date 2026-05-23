using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public record RemovePostingAccountCommand(long ProductId, ProductModuleType Module, PostingPurpose Purpose, CustomerSegment Segment)
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Products"; 
        public string AuditAction => "RemovePostingAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "ProductPostingAccount";
    }

}
