using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    public class CreateIndividualCommandHandler(IUnitOfWork uow, ISystemConfigurationService config, IUserContext userContext, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<CreateIndividualCommand, Result<IndividualDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly ISystemConfigurationService _config = config;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<IndividualDto>> Handle(CreateIndividualCommand command, CancellationToken token) {
            var logger = _loggerFactory.CreateLogger("kyc");
            logger.Channel = $"REGISTER-INDIVIDUAL-{command.Request.FirstName}-{command.Request.LastName}";
            logger.Log("Registering individual customer...", "KYC");

            var req = command.Request;

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                // Generate client code
                var clientCode = await _uow.Customers.GenerateClientCodeAsync(
                    CustomerType.Individual, req.BranchId, token);

                // Parse enums safely
                var gender = Enum.TryParse<Gender>(req.Gender, out var g) ? g : Gender.Other;
                var maritalStatus = req.MaritalStatus is not null &&
                                    Enum.TryParse<MaritalStatus>(req.MaritalStatus, out var ms) ? ms : (MaritalStatus?)null;

                // Read approval config
                var requireApproval = await _config.GetBoolAsync(
                    "CustomerKyc", "RequireClientApproval", defaultValue: true, token: token);
                var canTransactWithoutApproval = !requireApproval;

                var individual = new Individual
                {
                    ClientCode = clientCode,
                    FirstName = req.FirstName,
                    MiddleName = req.MiddleName,
                    LastName = req.LastName,
                    Gender = gender,
                    DateOfBirth = req.DateOfBirth,
                    BirthPlace = req.BirthPlace,
                    MaritalStatus = maritalStatus,
                    SpouseName = req.SpouseName,
                    Children = req.Children,
                    Dependents = req.Dependents,
                    Mother = req.Mother,
                    Father = req.Father,
                    Literate = req.Literate,
                    Photo = req.Photo,
                    Signature = req.Signature,
                    PermanentAddress = req.PermanentAddress,
                    MailAddress = req.MailAddress,
                    PrimaryLine = req.PrimaryLine,
                    Mobile = req.Mobile,
                    Email = req.Email,
                    City = req.City,
                    Town = req.Town,
                    WhatsApp = req.WhatsApp,
                    Notes = req.Notes,
                    Statistic = req.Statistic,
                    Reference = req.Reference,
                    BranchId = req.BranchId,
                    TitleId = req.TitleId,
                    NationalityId = req.NationalityId,
                    ProfessionId = req.ProfessionId,
                    EducationId = req.EducationId,
                    VillageId = req.VillageId,
                    Filter1Id = req.Filter1Id,
                    Filter2Id = req.Filter2Id,
                    Filter3Id = req.Filter3Id,
                    ClientType = ClientType.Individual,
                    RegisteredOn = DateTime.UtcNow,
                    Active = !requireApproval,  // auto-activate if no approval needed
                    Approved = !requireApproval,
                    ApprovedOn = requireApproval ? null : DateTime.UtcNow,
                    ApprovedBy = requireApproval ? null : _userContext.Username,
                    CanTransact = canTransactWithoutApproval,
                    HoldShares = false,
                    Exited = false
                };

                await _uow.Customers.AddIndividualAsync(individual, token);

                // Add contacts (next of kin)
                foreach (var c in req.Contacts) {
                    await _uow.Customers.AddContactAsync(new CustomerContact
                    {
                        CustomerId = individual.Id,
                        CustomerType = CustomerType.Individual,
                        ContactName = c.ContactName,
                        Telephone = c.Telephone,
                        Mobile = c.Mobile,
                        Email = c.Email,
                        Relationship = c.Relationship,
                        Notes = c.Notes
                    }, token);
                }

                // If approval required, create pending approval record
                if (requireApproval) {
                    await _uow.Customers.AddApprovalAsync(new CustomerApproval
                    {
                        CustomerId = individual.Id,
                        CustomerType = CustomerType.Individual,
                        Status = ApprovalStatus.Pending,
                        ActionedOn = DateTime.UtcNow,
                        ActionedBy = _userContext.Username,
                        Comments = "Pending approval"
                    }, token);
                }

                logger.Log(
                    $"Individual registered: {individual.ClientCode} — " +
                    $"{individual.FirstName} {individual.LastName} | Approval required: {requireApproval}",
                    "KYC-OK");

                return Result<IndividualDto>.Success(MapToDto(individual));
            }, token);
        }

        private static IndividualDto MapToDto(Individual i) => new()
        {
            Id = i.Id,
            ClientCode = i.ClientCode,
            Statistic = i.Statistic,
            Reference = i.Reference,
            FirstName = i.FirstName,
            MiddleName = i.MiddleName,
            LastName = i.LastName,
            Gender = i.Gender.ToString(),
            DateOfBirth = i.DateOfBirth,
            BirthPlace = i.BirthPlace,
            MaritalStatus = i.MaritalStatus?.ToString(),
            SpouseName = i.SpouseName,
            Children = i.Children,
            Dependents = i.Dependents,
            Mother = i.Mother,
            Father = i.Father,
            Literate = i.Literate,
            Photo = i.Photo,
            Signature = i.Signature,
            PermanentAddress = i.PermanentAddress,
            MailAddress = i.MailAddress,
            PrimaryLine = i.PrimaryLine,
            Mobile = i.Mobile,
            Email = i.Email,
            City = i.City,
            Town = i.Town,
            WhatsApp = i.WhatsApp,
            Notes = i.Notes,
            BranchId = i.BranchId,
            BranchName = i.Branch?.BranchName ?? string.Empty,
            Title = i.Title?.Name,
            Nationality = i.Nationality?.Name,
            Profession = i.Profession?.Name,
            Education = i.Education?.Name,
            Village = i.Village?.Name,
            Filter1 = i.Filter1?.Description,
            Filter2 = i.Filter2?.Description,
            Filter3 = i.Filter3?.Description,
            Active = i.Active,
            Approved = i.Approved,
            ApprovedOn = i.ApprovedOn,
            ApprovedBy = i.ApprovedBy,
            Exited = i.Exited,
            ExitedOn = i.ExitedOn,
            CanTransact = i.CanTransact,
            HoldShares = i.HoldShares,
            RegisteredOn = i.RegisteredOn
        };
    }

}
