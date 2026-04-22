using Argent.Api.Domain.Entities.Kyc;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    public class CreateBusinessCommandHandler(IUnitOfWork uow, ISystemConfigurationService config, IUserContext userContext, IServiceLoggerFactory loggerFactory)
                : IRequestHandler<CreateBusinessCommand, Result<BusinessDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly ISystemConfigurationService _config = config;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<BusinessDto>> Handle(
            CreateBusinessCommand command, CancellationToken token) {
            var logger = _loggerFactory.CreateLogger("kyc");
            logger.Channel = $"REGISTER-BUSINESS-{command.Request.LegalName}";
            var req = command.Request;

            // Enforce minimum signatories
            var minSignatories = await _config.GetIntAsync("CustomerKyc.Business", "NumberOfSignatoriesRequired", defaultValue: 2, token: token);
            if (req.Signatories.Count < minSignatories)
                return Result<BusinessDto>.Failure($"At least {minSignatories} signatory(ies) are required.", "INSUFFICIENT_SIGNATORIES");

            return await _uow.ExecuteInTransactionAsync(async token => {
                var clientCode = await _uow.Customers.GenerateClientCodeAsync( CustomerType.Business, req.BranchId, token);

                var requireApproval = await _config.GetBoolAsync("CustomerKyc", "RequireClientApproval", defaultValue: true, token: token);
                var business = new Business {
                    ClientCode = clientCode,
                    LegalName = req.LegalName,
                    PrimaryLine = req.PrimaryLine,
                    Mobile = req.Mobile,
                    Email = req.Email,
                    PermanentAddress = req.PermanentAddress,
                    MailAddress = req.MailAddress,
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
                    BusinessFilter1Id = req.BusinessFilter1Id,
                    BusinessFilter2Id = req.BusinessFilter2Id,
                    ClientType = ClientType.Business,
                    RegisteredOn = DateTime.UtcNow,
                    Active = !requireApproval,
                    Approved = !requireApproval,
                    ApprovedOn = requireApproval ? null : DateTime.UtcNow,
                    ApprovedBy = requireApproval ? null : _userContext.Username,
                    CanTransact = !requireApproval,
                    Exited = false
                };

                await _uow.Customers.AddBusinessAsync(business, token);

                // Add signatories
                foreach (var s in req.Signatories)
                    business.Signatories.Add(new Signatory
                    {
                        BusinessId = business.Id,
                        Code = $"SIG-{Guid.NewGuid().ToString()[..6].ToUpper()}",
                        Name = s.Name,
                        Position = s.Position,
                        Mobile = s.Mobile,
                        Email = s.Email,
                        CanSignAlone = s.CanSignAlone,
                        Photo = s.Photo,
                        Signature = s.Signature,
                        Notes = s.Notes,
                        Suspended = false
                    });

                // Add contacts
                foreach (var c in req.Contacts)
                    await _uow.Customers.AddContactAsync(new CustomerContact
                    {
                        CustomerId = business.Id,
                        CustomerType = CustomerType.Business,
                        ContactName = c.ContactName,
                        Mobile = c.Mobile,
                        Email = c.Email,
                        Relationship = c.Relationship
                    }, token);

                if (requireApproval)
                    await _uow.Customers.AddApprovalAsync(new CustomerApproval
                    {
                        CustomerId = business.Id,
                        CustomerType = CustomerType.Business,
                        Status = ApprovalStatus.Pending,
                        ActionedOn = DateTime.UtcNow,
                        ActionedBy = _userContext.Username,
                        Comments = "Pending approval"
                    }, token);

                logger.Log($"Business registered: {business.ClientCode} — {business.LegalName}", "KYC-OK");

                return Result<BusinessDto>.Success(new BusinessDto
                {
                    Id = business.Id,
                    ClientCode = business.ClientCode,
                    LegalName = business.LegalName,
                    Mobile = business.Mobile,
                    Email = business.Email,
                    PermanentAddress = business.PermanentAddress,
                    City = business.City,
                    Town = business.Town,
                    Notes = business.Notes,
                    BranchId = business.BranchId,
                    Active = business.Active,
                    Approved = business.Approved,
                    Exited = business.Exited,
                    CanTransact = business.CanTransact,
                    RegisteredOn = business.RegisteredOn,
                    SignatoryCount = business.Signatories.Count
                });
            }, token);
        }
    }
}
