using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetKycLookupsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetKycLookupsQuery, Result<KycLookupsDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<KycLookupsDto>> Handle(GetKycLookupsQuery query, CancellationToken token) {
            var (titles, nationalities, professions, education, income, positions, idTypes, issuers) = 
                (
                    await _uow.Customers.GetTitlesAsync(token),
                    await _uow.Customers.GetNationalitiesAsync(token),
                    await _uow.Customers.GetProfessionsAsync(token),
                    await _uow.Customers.GetEducationLevelsAsync(token),
                    await _uow.Customers.GetIncomeTypesAsync(token),
                    await _uow.Customers.GetGroupPositionsAsync(token),
                    await _uow.Customers.GetIdentificationTypesAsync(token),
                    await _uow.Customers.GetIssuerAuthoritiesAsync(token)
                );

            return Result<KycLookupsDto>.Success(new KycLookupsDto {
                Titles = titles.Select(t => new LookupItemDto { Id = t.Id, Name = t.Name }),
                Nationalities = nationalities.Select(n => new LookupItemDto { Id = n.Id, Name = n.Name, Code = n.Code }),
                Professions = professions.Select(p => new LookupItemDto { Id = p.Id, Name = p.Name, Code = p.Code }),
                EducationLevels = education.Select(e => new LookupItemDto { Id = e.Id, Name = e.Name, Code = e.Code }),
                IncomeTypes = income.Select(i => new LookupItemDto { Id = i.Id, Name = i.Name, Code = i.Code }),
                GroupPositions = positions.Select(p => new LookupItemDto { Id = p.Id, Name = p.Designation }),
                IdentificationTypes = idTypes.Select(i => new LookupItemDto { Id = i.Id, Name = i.TypeName }),
                IssuerAuthorities = issuers.Select(a => new LookupItemDto { Id = a.Id, Name = a.Name, Code = a.Code })
            });
        }
    }

}
