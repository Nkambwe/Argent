using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    public class CreateMemberCommandHandler(IUnitOfWork uow, ISystemConfigurationService config, IUserContext userContext)
                : IRequestHandler<CreateMemberCommand, Result<CustomerSummaryDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly ISystemConfigurationService _config = config;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<CustomerSummaryDto>> Handle(CreateMemberCommand command, CancellationToken token) {
            var req = command.Request;

            // Verify the group exists and belongs to the target branch
            var group = await _uow.Customers.GetGroupByIdAsync(req.GroupId, token);
            if (group is null)
                return Result<CustomerSummaryDto>.NotFound("Group not found.");

            if (group.BranchId != command.TargetBranchId)
                return Result<CustomerSummaryDto>.Failure("Group does not belong to the specified branch.", "BRANCH_MISMATCH");

            // Enforce max member count
            var maxMembers = await _config.GetIntAsync("CustomerKyc.Group", "MaxGroupMembers", defaultValue: 30, token: token);
            var currentCount = await _uow.Customers.GetGroupMemberCountAsync(req.GroupId, token);
            if (currentCount >= maxMembers)
                return Result<CustomerSummaryDto>.Failure($"Group has reached the maximum of {maxMembers} members.", "MAX_MEMBERS_REACHED");

            return await _uow.ExecuteInTransactionAsync(async token =>  {
                var clientCode = await _uow.Customers.GenerateClientCodeAsync(
                    CustomerType.Member, group.BranchId, token);

                var requireApproval = await _config.GetBoolAsync( "CustomerKyc", "RequireClientApproval", defaultValue: true, token: token);
                var gender = Enum.TryParse<Gender>(req.Gender, out var g) ? g : Gender.Other;
                var member = new Member {
                    GroupId = req.GroupId,
                    ClientCode = clientCode,
                    FirstName = req.FirstName,
                    MiddleName = req.MiddleName,
                    LastName = req.LastName,
                    Gender = gender,
                    DateOfBirth = req.DateOfBirth,
                    Mobile = req.Mobile,
                    Email = req.Email,
                    PermanentAddress = req.PermanentAddress,
                    Notes = req.Notes,
                    Photo = req.Photo,
                    Signature = req.Signature,
                    BranchId = group.BranchId,
                    TitleId = req.TitleId,
                    NationalityId = req.NationalityId,
                    ProfessionId = req.ProfessionId,
                    EducationId = req.EducationId,
                    VillageId = req.VillageId,
                    Filter1Id = req.Filter1Id,
                    Filter2Id = req.Filter2Id,
                    Filter3Id = req.Filter3Id,
                    MemberFilter1Id = req.MemberFilter1Id,
                    MemberFilter2Id = req.MemberFilter2Id,
                    ClientType = ClientType.GroupMember,
                    RegisteredOn = DateTime.UtcNow,
                    JoinedOn = DateTime.UtcNow,
                    Active = !requireApproval,
                    Approved = !requireApproval,
                    ApprovedOn = requireApproval ? null : DateTime.UtcNow,
                    ApprovedBy = requireApproval ? null : _userContext.Username,
                    CanTransact = !requireApproval,
                    Exited = false
                };

                await _uow.Customers.AddMemberAsync(member, token);

                foreach (var c in req.Contacts)
                    await _uow.Customers.AddContactAsync(new CustomerContact
                    {
                        CustomerId = member.Id,
                        CustomerType = CustomerType.Member,
                        ContactName = c.ContactName,
                        Mobile = c.Mobile,
                        Email = c.Email,
                        Relationship = c.Relationship
                    }, token);

                return Result<CustomerSummaryDto>.Success(new CustomerSummaryDto {
                    Id = member.Id,
                    ClientCode = member.ClientCode,
                    CustomerType = CustomerType.Member,
                    DisplayName = $"{member.FirstName} {member.LastName}".Trim(),
                    Mobile = member.Mobile,
                    Email = member.Email,
                    BranchName = group.Branch?.BranchName ?? string.Empty,
                    Active = member.Active,
                    Approved = member.Approved,
                    Exited = member.Exited,
                    CanTransact = member.CanTransact,
                    RegisteredOn = member.RegisteredOn
                });
            }, token);
        }
    }
}
