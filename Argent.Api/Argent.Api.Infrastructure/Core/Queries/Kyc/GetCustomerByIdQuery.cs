using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Kyc {

    public record GetCustomerByIdQuery(long CustomerId, CustomerType CustomerType) : IRequest<Result<object>>;

    public class GetCustomerByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetCustomerByIdQuery, Result<object>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<object>> Handle(GetCustomerByIdQuery query, CancellationToken token) {
            switch (query.CustomerType) {
                case CustomerType.Individual: {
                        var individual = await _uow.Customers.GetIndividualByIdAsync(query.CustomerId, token);
                        if (individual is null)
                            return Result<object>.NotFound("Individual customer not found.");

                        return Result<object>.Success(new IndividualDto
                        {
                            Id = individual.Id,
                            ClientCode = individual.ClientCode,
                            Statistic = individual.Statistic,
                            Reference = individual.Reference,
                            FirstName = individual.FirstName,
                            MiddleName = individual.MiddleName,
                            LastName = individual.LastName,
                            Gender = individual.Gender.ToString(),
                            DateOfBirth = individual.DateOfBirth,
                            BirthPlace = individual.BirthPlace,
                            MaritalStatus = individual.MaritalStatus?.ToString(),
                            SpouseName = individual.SpouseName,
                            Children = individual.Children,
                            Dependents = individual.Dependents,
                            Mother = individual.Mother,
                            Father = individual.Father,
                            Literate = individual.Literate,
                            Photo = individual.Photo,
                            Signature = individual.Signature,
                            PermanentAddress = individual.PermanentAddress,
                            MailAddress = individual.MailAddress,
                            PrimaryLine = individual.PrimaryLine,
                            Mobile = individual.Mobile,
                            Email = individual.Email,
                            City = individual.City,
                            Town = individual.Town,
                            WhatsApp = individual.WhatsApp,
                            Notes = individual.Notes,
                            BranchId = individual.BranchId,
                            BranchName = individual.Branch?.BranchName ?? string.Empty,
                            Title = individual.Title?.Name,
                            Nationality = individual.Nationality?.Name,
                            Profession = individual.Profession?.Name,
                            Education = individual.Education?.Name,
                            Village = individual.Village?.Name,
                            Filter1 = individual.Filter1?.Description,
                            Filter2 = individual.Filter2?.Description,
                            Filter3 = individual.Filter3?.Description,
                            Active = individual.Active,
                            Approved = individual.Approved,
                            ApprovedOn = individual.ApprovedOn,
                            ApprovedBy = individual.ApprovedBy,
                            Exited = individual.Exited,
                            ExitedOn = individual.ExitedOn,
                            CanTransact = individual.CanTransact,
                            HoldShares = individual.HoldShares,
                            RegisteredOn = individual.RegisteredOn,
                            Contacts = individual.Contacts.Select(c => new ContactDto
                            {
                                Id = c.Id,
                                ContactName = c.ContactName,
                                Telephone = c.Telephone,
                                Mobile = c.Mobile,
                                Email = c.Email,
                                Relationship = c.Relationship
                            })
                        });
                    }

                case CustomerType.Group: {
                        var group = await _uow.Customers.GetGroupByIdAsync(query.CustomerId, token);
                        if (group is null)
                            return Result<object>.NotFound("Group not found.");

                        var memberCount = await _uow.Customers.GetGroupMemberCountAsync(query.CustomerId, token);
                        return Result<object>.Success(new GroupDto
                        {
                            Id = group.Id,
                            ClientCode = group.ClientCode,
                            RegisteredName = group.RegisteredName,
                            PermanentAddress = group.PermanentAddress,
                            Mobile = group.Mobile,
                            Email = group.Email,
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
                            MemberCount = memberCount,
                            GroupFilter1 = group.GroupFilter1?.Description,
                            GroupFilter2 = group.GroupFilter2?.Description,
                            Contacts = group.Contacts.Select(c => new ContactDto
                            {
                                Id = c.Id,
                                ContactName = c.ContactName,
                                Mobile = c.Mobile,
                                Email = c.Email,
                                Relationship = c.Relationship
                            })
                        });
                    }

                case CustomerType.Business: {
                        var business = await _uow.Customers.GetBusinessByIdAsync(query.CustomerId, token);
                        if (business is null)
                            return Result<object>.NotFound("Business customer not found.");

                        return Result<object>.Success(new BusinessDto
                        {
                            Id = business.Id,
                            ClientCode = business.ClientCode,
                            LegalName = business.LegalName,
                            PrimaryLine = business.PrimaryLine,
                            Mobile = business.Mobile,
                            Email = business.Email,
                            PermanentAddress = business.PermanentAddress,
                            City = business.City,
                            Town = business.Town,
                            Notes = business.Notes,
                            BranchId = business.BranchId,
                            BranchName = business.Branch?.BranchName ?? string.Empty,
                            Active = business.Active,
                            Approved = business.Approved,
                            Exited = business.Exited,
                            CanTransact = business.CanTransact,
                            RegisteredOn = business.RegisteredOn,
                            SignatoryCount = business.Signatories.Count,
                            BusinessFilter1 = business.BusinessFilter1?.Description,
                            BusinessFilter2 = business.BusinessFilter2?.Description,
                            Contacts = business.Contacts.Select(c => new ContactDto
                            {
                                Id = c.Id,
                                ContactName = c.ContactName,
                                Mobile = c.Mobile,
                                Email = c.Email,
                                Relationship = c.Relationship
                            }),
                            Signatories = business.Signatories.Select(s => new SignatoryDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Position = s.Position,
                                Mobile = s.Mobile,
                                CanSignAlone = s.CanSignAlone,
                                Suspended = s.Suspended
                            })
                        });
                    }

                default:
                    return Result<object>.Failure($"Unsupported customer type: {query.CustomerType}", "UNSUPPORTED_TYPE");
            }
        }
    }

}
