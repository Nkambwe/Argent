using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    public class CreateGroupCommandHandler(
        IUnitOfWork uow, ISystemConfigurationService config,
        IUserContext userContext, IServiceLoggerFactory loggerFactory) : IRequestHandler<CreateGroupCommand, Result<GroupDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly ISystemConfigurationService _config = config;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<GroupDto>> Handle(CreateGroupCommand command, CancellationToken token) {
            var logger = _loggerFactory.CreateLogger("kyc");
            logger.Channel = $"REGISTER-GROUP-{command.Request.RegisteredName}";

            var req = command.Request;

            return await _uow.ExecuteInTransactionAsync(async token => {
                var clientCode = await _uow.Customers.GenerateClientCodeAsync(
                    CustomerType.Group, req.BranchId, token);

                var requireApproval = await _config.GetBoolAsync("CustomerKyc", "RequireClientApproval", defaultValue: true, token: token);
                var group = new Group {
                    ClientCode = clientCode,
                    RegisteredName = req.RegisteredName,
                    Mobile = req.Mobile,
                    Email = req.Email,
                    PermanentAddress = req.PermanentAddress,
                    City = req.City,
                    Town = req.Town,
                    WhatsApp = req.WhatsApp,
                    Notes = req.Notes,
                    Statistic = req.Statistic,
                    Reference = req.Reference,
                    BranchId = req.BranchId,
                    VillageId = req.VillageId,
                    Filter1Id = req.Filter1Id,
                    Filter2Id = req.Filter2Id,
                    Filter3Id = req.Filter3Id,
                    GroupFilter1Id = req.GroupFilter1Id,
                    GroupFilter2Id = req.GroupFilter2Id,
                    ClientType = ClientType.Group,
                    RegisteredOn = DateTime.UtcNow,
                    Active = !requireApproval,
                    Approved = !requireApproval,
                    ApprovedOn = requireApproval ? null : DateTime.UtcNow,
                    ApprovedBy = requireApproval ? null : _userContext.Username,
                    CanTransact = !requireApproval,
                    Exited = false
                };

                await _uow.Customers.AddGroupAsync(group, token);

                foreach (var c in req.Contacts)
                    await _uow.Customers.AddContactAsync(new CustomerContact
                    {
                        CustomerId = group.Id,
                        CustomerType = CustomerType.Group,
                        ContactName = c.ContactName,
                        Mobile = c.Mobile,
                        Email = c.Email,
                        Relationship = c.Relationship,
                        Notes = c.Notes
                    }, token);

                if (requireApproval)
                    await _uow.Customers.AddApprovalAsync(new CustomerApproval
                    {
                        CustomerId = group.Id,
                        CustomerType = CustomerType.Group,
                        Status = ApprovalStatus.Pending,
                        ActionedOn = DateTime.UtcNow,
                        ActionedBy = _userContext.Username,
                        Comments = "Pending approval"
                    }, token);

                logger.Log($"Group registered: {group.ClientCode} — {group.RegisteredName}", "KYC-OK");

                return Result<GroupDto>.Success(new GroupDto {
                    Id = group.Id,
                    ClientCode = group.ClientCode,
                    RegisteredName = group.RegisteredName,
                    Mobile = group.Mobile,
                    Email = group.Email,
                    PermanentAddress = group.PermanentAddress,
                    City = group.City,
                    Town = group.Town,
                    Notes = group.Notes,
                    BranchId = group.BranchId,
                    BranchName = group.Branch?.BranchName ?? string.Empty,
                    Active = group.Active,
                    Approved = group.Approved,
                    Exited = group.Exited,
                    CanTransact = group.CanTransact,
                    RegisteredOn = group.RegisteredOn,
                    MemberCount = 0
                });
            }, token);
        }
    }
}
