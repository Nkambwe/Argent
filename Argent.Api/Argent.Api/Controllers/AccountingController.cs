using Argent.Api.Infrastructure.Core.Commands.Accounting;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class AccountingController(IMediator mediator, IUnitOfWork uow, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        /// <summary>
        /// List all charts of accounts.
        /// </summary>
        [HttpGet("charts")]
        [ProducesResponseType(typeof(IEnumerable<AccountsChartDto>), 200)]
        public async Task<IActionResult> GetCharts(CancellationToken ct) {
            var result = await _mediator.Send(new GetAccountsChartQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// List creatable account headers (SubCategory and Header types only).
        /// Label, Category, and total rows are system-managed and not returned here.
        /// </summary>
        [HttpGet("headers")]
        [ProducesResponseType(typeof(IEnumerable<LedgerAccountHeaderDto>), 200)]
        public async Task<IActionResult> GetHeaders([FromQuery] string? classification, [FromQuery] string? nature,
            CancellationToken ct = default) {
            var result = await _mediator.Send(new GetLedgerHeadersQuery(classification, nature), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific header by ID.
        /// </summary>
        [HttpGet("headers/{id:long}")]
        [ProducesResponseType(typeof(LedgerAccountHeaderDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetHeaderById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetLedgerHeaderByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new account header. Only SubCategory and Header types are allowed.
        /// Label and Category rows are system-managed.
        /// </summary>
        [HttpPost("headers")]
        [ProducesResponseType(typeof(LedgerAccountHeaderDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateHeader([FromBody] CreateLedgerHeaderRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateLedgerHeaderCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_NUMBER"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetHeaderById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>Update a header's name, parent, or sort order.</summary>
        [HttpPut("headers/{id:long}")]
        [ProducesResponseType(typeof(LedgerAccountHeaderDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateHeader(long id, [FromBody] UpdateLedgerHeaderRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateLedgerHeaderCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Delete a header. Blocked if it has child headers or ledger accounts.
        /// System-managed headers cannot be deleted.
        /// </summary>
        [HttpDelete("headers/{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteHeader(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteLedgerHeaderCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Header deleted." })
                : result.ErrorCode is "SYSTEM_MANAGED" or "HAS_ACCOUNTS" or "HAS_CHILDREN"
                    ? BadRequest(new { result.Error })
                    : NotFound(new { result.Error });
        }

        /// <summary>
        /// List postable ledger accounts with optional filtering.
        /// </summary>
        [HttpGet("ledgers")]
        [ProducesResponseType(typeof(PagedResult<LedgerAccountDto>), 200)]
        public async Task<IActionResult> GetLedgerAccounts([FromQuery] LedgerSearchRequest request, CancellationToken ct = default) {
            var result = await _mediator.Send(new GetLedgerAccountsQuery(request), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific ledger account by ID.
        /// </summary>
        [HttpGet("ledgers/{id:long}")]
        [ProducesResponseType(typeof(LedgerAccountDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLedgerAccountById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetLedgerAccountByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new postable ledger account. Always created as AccountCategory.Ledger.
        /// </summary>
        [HttpPost("ledgers")]
        [ProducesResponseType(typeof(LedgerAccountDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateLedgerAccount([FromBody] CreateLedgerAccountRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateLedgerAccountCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_NUMBER"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetLedgerAccountById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update a ledger account's properties.
        /// </summary>
        [HttpPut("ledgers/{id:long}")]
        [ProducesResponseType(typeof(LedgerAccountDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateLedgerAccount(long id, [FromBody] UpdateLedgerAccountRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateLedgerAccountCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Suspend a ledger account. Blocks new GL postings while preserving all history.
        /// Prefer this over deletion when the account has existing transactions.
        /// </summary>
        [HttpPut("ledgers/{id:long}/suspend")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SuspendLedgerAccount(long id, CancellationToken ct) {
            var result = await _mediator.Send(new SuspendLedgerAccountCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Account suspended. No new postings allowed." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Reactivate a suspended ledger account.
        /// </summary>
        [HttpPut("ledgers/{id:long}/activate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ActivateLedgerAccount(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateLedgerAccountCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Account reactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Delete a ledger account. Blocked if the account has a non-zero balance
        /// or any GL transactions. Suspend instead when transactions exist.
        /// </summary>
        [HttpDelete("ledgers/{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteLedgerAccount(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteLedgerAccountCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Account deleted." })
                    : result.ErrorCode is "NON_ZERO_BALANCE" or "HAS_TRANSACTIONS"
                    ? BadRequest(new { result.Error })
                    : NotFound(new { result.Error });
        }
    }
}
