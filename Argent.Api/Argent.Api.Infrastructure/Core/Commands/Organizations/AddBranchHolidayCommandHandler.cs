using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public class AddBranchHolidayCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<AddBranchHolidayCommand, Result<BranchHolidayDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<BranchHolidayDto>> Handle(
            AddBranchHolidayCommand command, CancellationToken ct) {
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);
            if (branch is null)
                return Result<BranchHolidayDto>.NotFound("Branch not found.");

            var req = command.Request;
            var holiday = new BranchHoliday
            {
                BranchId = command.BranchId,
                Name = req.Name,
                HolidayDate = req.Date,
                Recurrence = req.Recurrence,
                IsActive = req.IsActive,
                Notes = req.Notes,
                CreatedBy = _userContext.Username
            };

            await _uow.Organizations.AddHolidayAsync(holiday, ct);
            await _uow.CommitAsync(ct);

            return Result<BranchHolidayDto>.Success(new BranchHolidayDto {
                Id = holiday.Id,
                BranchId = holiday.BranchId,
                Name = holiday.Name,
                Date = holiday.HolidayDate,
                Recurrence = holiday.Recurrence,
                IsActive = holiday.IsActive,
                Notes = holiday.Notes
            });
        }
    }
}
