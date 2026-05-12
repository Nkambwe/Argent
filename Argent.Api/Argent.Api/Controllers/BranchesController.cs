using Argent.Api.Infrastructure.Core.Commands.Organizations;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/branches")]
    [Authorize]
    [Produces("application/json")]
    public class BranchesController(IMediator mediator, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUserContext _userContext = userContext;

        [HttpGet("get-branches")]
        [ProducesResponseType(typeof(IEnumerable<BranchDto>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken ct) {
            var result = await _mediator.Send(new GetBranchesQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("get-branch{id:long}")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetBranchByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost("create-branch")]
        [ProducesResponseType(typeof(BranchDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateBranchRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateBranchCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("update-branch/{id:long}")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateBranchRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateBranchCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Set this branch as the default branch.
        /// Clears the default flag from any previously default branch.
        /// </summary>
        [HttpPut("set-default/{id:long}")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SetDefault(long id, CancellationToken ct) {
            var result = await _mediator.Send(new SetDefaultBranchCommand(id), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Deactivate (soft-delete) a branch.
        /// Cannot deactivate the default branch — set another as default first.
        /// </summary>
        [HttpDelete("deactivate-branch/{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Deactivate(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateBranchCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Branch deactivated." })
                : result.ErrorCode == "DEFAULT_BRANCH" ? BadRequest(new { result.Error })
                : NotFound(new { result.Error });
        }

        #region Holidays

        /// <summary>
        /// Add a holiday to a branch. Recurring holidays repeat every year on the same date.
        /// </summary>
        [HttpPost("add-holiday/{id:long}")]
        [ProducesResponseType(typeof(BranchHolidayDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddHoliday(long id, [FromBody] AddHolidayRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AddBranchHolidayCommand(id, request), ct);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id }, result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove a branch holiday bt a hard delete. Hard Delete leaves no audit dependency.
        /// </summary>
        [HttpDelete("remove-branch-holiday/{id:long}/holidays/{holidayId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveHoliday(long id, long holidayId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveBranchHolidayCommand(holidayId), ct);
            return result.IsSuccess ? Ok(new { Message = "Holiday removed." })
                : NotFound(new { result.Error });
        }

        #endregion
    }
}
