using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using Argent.Api.Infrastructure.Core.Validation.Kyc;
using Argent.Api.Infrastructure.Core.Commands.Kyc;
using Argent.Api.Infrastructure.Core.Queries;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class CustomerController(IMediator mediator, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUserContext _userContext = userContext;

        #region LOOKUPS

        /// <summary>
        /// Returns all KYC reference data needed to populate registration forms
        /// (titles, nationalities, professions, education levels, etc.)
        /// </summary>
        [HttpGet("lookups")]
        [ProducesResponseType(typeof(KycLookupsDto), 200)]
        public async Task<IActionResult> GetLookups(CancellationToken ct) {
            var result = await _mediator.Send(new GetKycLookupsQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        #endregion

        #region INDIVIDUALS

        /// <summary>
        /// List individual customers with optional filters.
        /// Results are limited to branches the calling user can access.
        /// </summary>
        [HttpGet("individuals")]
        [ProducesResponseType(typeof(PagedResult<CustomerSummaryDto>), 200)]
        public async Task<IActionResult> GetIndividuals([FromQuery] long? branchId, [FromQuery] bool? active, [FromQuery] bool? approved,
            [FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default) {
            var result = await _mediator.Send(new GetCustomersQuery(CustomerType.Individual, branchId, active, approved, search, page, pageSize), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific individual customer by ID.
        /// </summary>
        [HttpGet("individuals/{id:long}")]
        [ProducesResponseType(typeof(IndividualDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetIndividual(long id, CancellationToken token) {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id, CustomerType.Individual), token);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Register a new individual customer.
        /// </summary>
        [HttpPost("individuals")]
        [ProducesResponseType(typeof(IndividualDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateIndividual([FromBody] CreateIndividualRequest request, CancellationToken token) {
            var result = await _mediator.Send(new CreateIndividualCommand(request, request.BranchId), token);

            if (!result.IsSuccess)
                return result.ErrorCode == "BRANCH_ACCESS_DENIED" ? Forbid() : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetIndividual), new { id = result.Data!.Id }, result.Data);
        }

        #endregion

        #region GROUPS

        /// <summary>
        /// List group customers.
        /// </summary>
        [HttpGet("groups")]
        [ProducesResponseType(typeof(PagedResult<CustomerSummaryDto>), 200)]
        public async Task<IActionResult> GetGroups([FromQuery] long? branchId, [FromQuery] bool? active, [FromQuery] bool? approved,
            [FromQuery] string? search,[FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken token = default) {
            var result = await _mediator.Send(new GetCustomersQuery(CustomerType.Group, branchId, active, approved, search, page, pageSize), token);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific group by ID.
        /// </summary>
        [HttpGet("groups/{id:long}")]
        [ProducesResponseType(typeof(GroupDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGroup(long id, CancellationToken token) {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id, CustomerType.Group), token);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Register a new group.
        /// </summary>
        [HttpPost("groups")]
        [ProducesResponseType(typeof(GroupDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request, CancellationToken token) {
            var result = await _mediator.Send(new CreateGroupCommand(request, request.BranchId), token);

            if (!result.IsSuccess)
                return result.ErrorCode == "BRANCH_ACCESS_DENIED" ? Forbid() : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetGroup), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// List members of a group.
        /// </summary>
        [HttpGet("groups/{groupId:long}/members")]
        [ProducesResponseType(typeof(PagedResult<CustomerSummaryDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGroupMembers(long groupId, [FromQuery] bool? active, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20, CancellationToken ct = default) {
            var result = await _mediator.Send(new GetGroupMembersQuery(groupId, active, page, pageSize), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Register a new member in a group.
        /// </summary>
        [HttpPost("groups/{groupId:long}/members")]
        [ProducesResponseType(typeof(CustomerSummaryDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddMember(long groupId, [FromBody] CreateMemberRequest request, CancellationToken ct) {
            request.GroupId = groupId;
            var result = await _mediator.Send(new CreateMemberCommand(request, _userContext.CurrentBranchId), ct);

            return result.IsSuccess ? StatusCode(201, result.Data) : result.ErrorCode 
                switch
                {
                    "NOT_FOUND" => NotFound(new { result.Error }),
                    "MAX_MEMBERS_REACHED" => BadRequest(new { result.Error }),
                    _ => BadRequest(new { result.Error })
                };
        }

        #endregion

        #region BUSINESSES

        /// <summary>
        /// List business customers.
        /// </summary>
        [HttpGet("businesses")]
        [ProducesResponseType(typeof(PagedResult<CustomerSummaryDto>), 200)]
        public async Task<IActionResult> GetBusinesses([FromQuery] long? branchId, [FromQuery] bool? active, [FromQuery] bool? approved, 
            [FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken token = default) {
            var result = await _mediator.Send(new GetCustomersQuery(CustomerType.Business, branchId, active, approved, search, page, pageSize), token);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific business by ID.
        /// </summary>
        [HttpGet("businesses/{id:long}")]
        [ProducesResponseType(typeof(BusinessDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBusiness(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id, CustomerType.Business), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Register a new business customer.
        /// </summary>
        [HttpPost("businesses")]
        [ProducesResponseType(typeof(BusinessDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request, CancellationToken token) {
            var result = await _mediator.Send(new CreateBusinessCommand(request, request.BranchId), token);

            if (!result.IsSuccess)
                return result.ErrorCode == "BRANCH_ACCESS_DENIED" ? Forbid() : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetBusiness), new { id = result.Data!.Id }, result.Data);
        }

        #endregion

        #region LIFECYCLE - shared across all types

        /// <summary>
        /// Approve a customer registration. Requires CustomerKyc.ApproveIndividual permission.
        /// The system will reject self-approval based on SystemConfig.CanApproveOwnRegistrations.
        /// </summary>
        [HttpPatch("{customerType}/{id:long}/approve")]
        [ProducesResponseType(typeof(CustomerSummaryDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Approve(string customerType, long id, [FromBody] ApproveCustomerRequest request, CancellationToken token) {
            if (!Enum.TryParse<CustomerType>(customerType, true, out var type))
                return BadRequest(new { Error = $"Unknown customer type: {customerType}" });

            var result = await _mediator.Send(new ApproveCustomerCommand(id, type, request.Comments, _userContext.CurrentBranchId), token);
            return result.IsSuccess ? Ok(result.Data) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Blacklist a customer. Immediately suspends their ability to transact.
        /// </summary>
        [HttpPatch("{customerType}/{id:long}/blacklist")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Blacklist(string customerType, long id, [FromBody] BlacklistCustomerRequest request, CancellationToken token) {
            if (!Enum.TryParse<CustomerType>(customerType, true, out var type))
                return BadRequest(new { Error = $"Unknown customer type: {customerType}" });

            var result = await _mediator.Send(new BlacklistCustomerCommand(id, type, request.ReasonId, request.Notes, _userContext.CurrentBranchId), token);
            return result.IsSuccess ? Ok(new { Message = "Customer has been blacklisted." }) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Record a customer exit when leaving the organization.
        /// </summary>
        [HttpPatch("{customerType}/{id:long}/exit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Exit(string customerType, long id, [FromBody] ExitCustomerRequest request, CancellationToken ct) {
            if (!Enum.TryParse<CustomerType>(customerType, true, out var type))
                return BadRequest(new { Error = $"Unknown customer type: {customerType}" });

            var result = await _mediator.Send(new ExitCustomerCommand(id, type, request.ReasonId, request.Notes, _userContext.CurrentBranchId), ct);
            return result.IsSuccess ? Ok(new { Message = "Customer exit has been recorded." }) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Reject a pending customer registration.
        /// </summary>
        [HttpPatch("{customerType}/{id:long}/reject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Reject(string customerType, long id, [FromBody] RejectCustomerRequest request, CancellationToken ct) {
            if (!Enum.TryParse<CustomerType>(customerType, true, out var type))
                return BadRequest(new { Error = $"Unknown customer type: {customerType}" });

            var result = await _mediator.Send(new RejectCustomerCommand(id, type, request.ReasonId, request.Notes, _userContext.CurrentBranchId), ct);
            return result.IsSuccess ? Ok(new { Message = "Customer registration has been rejected." }) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

        #endregion
    }

}
