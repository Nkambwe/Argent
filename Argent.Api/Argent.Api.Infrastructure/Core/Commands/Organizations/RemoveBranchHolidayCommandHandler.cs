using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public class RemoveBranchHolidayCommandHandler
        : IRequestHandler<RemoveBranchHolidayCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public RemoveBranchHolidayCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result> Handle(
            RemoveBranchHolidayCommand command, CancellationToken ct) {
            var holiday = await _uow.Organizations.GetHolidayByIdAsync(command.HolidayId, ct);
            if (holiday is null)
                return Result.Failure("Holiday not found.", "NOT_FOUND");

            // Hard delete is acceptable for holidays — no downstream audit dependency
            _uow.Organizations.RemoveHoliday(holiday);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }
}
