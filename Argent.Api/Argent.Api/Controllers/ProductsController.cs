using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Commands.Products;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/products")]
    [Authorize]
    [Produces("application/json")]
    public class ProductsController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        #region Product Types

        /// <summary>
        /// List all product types, optionally filtered by module.
        /// </summary>
        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes([FromQuery] ProductModuleType? module, CancellationToken ct) {
            var result = await _mediator.Send(new GetProductTypesQuery(module), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a specific product type.
        /// </summary>
        [HttpGet("types/{id:guid}")]
        public async Task<IActionResult> GetProductTypeById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetProductTypeByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new product type.
        /// </summary>
        [HttpPost("types")]
        [ProducesResponseType(typeof(ProductTypeDto), 201)]
        public async Task<IActionResult> CreateProductType([FromBody] CreateProductTypeRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateProductTypeCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE" ? Conflict(new { result.Error }) : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetProductTypeById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update a product type. System types cannot be modified.
        /// </summary>
        [HttpPut("types/{id:long}")]
        public async Task<IActionResult> UpdateProductType(long id, [FromBody] UpdateProductTypeRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateProductTypeCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Delete a product type. System types cannot be deleted.
        /// </summary>
        [HttpDelete("types/{id:long}")]
        public async Task<IActionResult> DeleteProductType(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteProductTypeCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Product type deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region Posting Accounts

        /// <summary>
        /// List all GL posting account mappings for a product.
        /// </summary>
        [HttpGet("{module}/{productId:long}/posting-accounts")]
        public async Task<IActionResult> GetPostingAccounts(ProductModuleType module, long productId, CancellationToken ct) {
            var result = await _mediator.Send(new GetProductPostingAccountsQuery(productId, module), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Create or update a GL posting account mapping for a product.
        /// Uses upsert semantics — if a mapping for the same purpose/segment exists it is updated,
        /// otherwise a new one is created.
        /// </summary>
        [HttpPut("{module}/{productId:long}/posting-accounts")]
        public async Task<IActionResult> UpsertPostingAccount(ProductModuleType module, long productId, [FromBody] UpsertPostingAccountRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpsertPostingAccountCommand(productId, module, request), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove a GL posting account mapping from a product.
        /// </summary>
        [HttpDelete("{module}/{productId:long}/posting-accounts")]
        public async Task<IActionResult> RemovePostingAccount(ProductModuleType module, long productId, [FromQuery] PostingPurpose purpose, [FromQuery] CustomerSegment segment, CancellationToken ct) {
            var result = await _mediator.Send(new RemovePostingAccountCommand(productId, module, purpose, segment), ct);
            return result.IsSuccess ? Ok(new { Message = "Posting account mapping removed." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region SAVING PRODUCTS

        /// <summary>
        /// List all saving products.
        /// </summary>
        [HttpGet("savings")]
        public async Task<IActionResult> GetSavingProducts([FromQuery] bool? isActive, CancellationToken ct) {
            var result = await _mediator.Send(new GetSavingProductsQuery(isActive), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a saving product with full detail including configuration and posting accounts.
        /// </summary>
        [HttpGet("savings/{id:long}")]
        public async Task<IActionResult> GetSavingProductById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetSavingProductByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new saving product.
        /// Configuration is seeded from module defaults on creation.
        /// Use ConfigOverrides to set specific initial values.
        /// PostingAccounts should include mappings for Deposits, InterestExpense, etc.
        /// </summary>
        [HttpPost("savings")]
        [ProducesResponseType(typeof(SavingProductDetailDto), 201)]
        public async Task<IActionResult> CreateSavingProduct([FromBody] CreateSavingProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateSavingProductCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetSavingProductById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>Update a saving product's core fields.</summary>
        [HttpPut("savings/{id:long}")]
        public async Task<IActionResult> UpdateSavingProduct(long id, [FromBody] UpdateSavingProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateSavingProductCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Update a saving product's full configuration.
        /// Controls interest, overdraft, dormancy, withdrawals, cheque books,
        /// standing orders, SMS banking, electronic cards, and per-segment minimum balances.
        /// </summary>
        [HttpPut("savings/{id:long}/configuration")]
        public async Task<IActionResult> UpdateSavingProductConfig(long id, [FromBody] UpdateSavingProductConfigRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateSavingProductConfigCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Activate a saving product so new accounts can be opened under it.
        /// </summary>
        [HttpPut("savings/{id:guid}/activate")]
        public async Task<IActionResult> ActivateSavingProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateSavingProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Saving product activated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Deactivate a saving product. Existing accounts continue to operate;
        /// no new accounts can be opened under the product.
        /// </summary>
        [HttpPut("savings/{id:long}/deactivate")]
        public async Task<IActionResult> DeactivateSavingProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateSavingProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Saving product deactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>Delete a saving product. Must be deactivated first.</summary>
        [HttpDelete("savings/{id:guid}")]
        public async Task<IActionResult> DeleteSavingProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteSavingProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Saving product deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region LOAN PRODUCTS

        [HttpGet("loans")]
        public async Task<IActionResult> GetLoanProducts([FromQuery] bool? isActive, CancellationToken ct) {
            var result = await _mediator.Send(new GetLoanProductsQuery(isActive), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("loans/{id:long}")]
        public async Task<IActionResult> GetLoanProductById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetLoanProductByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost("loans")]
        [ProducesResponseType(typeof(LoanProductDetailDto), 201)]
        public async Task<IActionResult> CreateLoanProduct([FromBody] CreateLoanProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateLoanProductCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetLoanProductById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("loans/{id:long}")]
        public async Task<IActionResult> UpdateLoanProduct(long id, [FromBody] UpdateLoanProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateLoanProductCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("loans/{id:long}/activate")]
        public async Task<IActionResult> ActivateLoanProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateLoanProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Loan product activated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("loans/{id:long}/deactivate")]
        public async Task<IActionResult> DeactivateLoanProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateLoanProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Loan product deactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("loans/{id:long}")]
        public async Task<IActionResult> DeleteLoanProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteLoanProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Loan product deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region SHARE PRODUCTS

        [HttpGet("shares")]
        public async Task<IActionResult> GetShareProducts([FromQuery] bool? isActive, CancellationToken ct) {
            var result = await _mediator.Send(new GetShareProductsQuery(isActive), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("shares/{id:long}")]
        public async Task<IActionResult> GetShareProductById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetShareProductByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost("shares")]
        [ProducesResponseType(typeof(ShareProductDetailDto), 201)]
        public async Task<IActionResult> CreateShareProduct([FromBody] CreateShareProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateShareProductCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetShareProductById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("shares/{id:long}")]
        public async Task<IActionResult> UpdateShareProduct(long id, [FromBody] UpdateShareProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateShareProductCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Update share product configuration — dividend method, rates, redemption rules.
        /// </summary>
        [HttpPut("shares/{id:long}/configuration")]
        public async Task<IActionResult> UpdateShareProductConfig(long id, [FromBody] UpdateShareProductConfigRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateShareProductConfigCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("shares/{id:long}/activate")]
        public async Task<IActionResult> ActivateShareProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateShareProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Share product activated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("shares/{id:long}/deactivate")]
        public async Task<IActionResult> DeactivateShareProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateShareProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Share product deactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("shares/{id:long}")]
        public async Task<IActionResult> DeleteShareProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteShareProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Share product deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region TIME DEPOSIT PRODUCTS

        [HttpGet("timedeposits")]
        public async Task<IActionResult> GetTimedepositProducts([FromQuery] bool? isActive, CancellationToken ct) {
            var result = await _mediator.Send(new GetTimedepositProductsQuery(isActive), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("timedeposits/{id:long}")]
        public async Task<IActionResult> GetTimedepositProductById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetTimedepositProductByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost("timedeposits")]
        [ProducesResponseType(typeof(TimedepositProductDetailDto), 201)]
        public async Task<IActionResult> CreateTimedepositProduct([FromBody] CreateTimedepositProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateTimedepositProductCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetTimedepositProductById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("timedeposits/{id:long}")]
        public async Task<IActionResult> UpdateTimedepositProduct(long id, [FromBody] UpdateTimedepositProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateTimedepositProductCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Add an interest rate entry for a flat-schedule time deposit product.
        /// Not applicable when TierInterest = true — use /tiers instead.
        /// </summary>
        [HttpPost("timedeposits/{id:long}/rates")]
        public async Task<IActionResult> AddTimedepositRate(long id, [FromBody] CreateTimedepositRateRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AddTimedepositRateCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove an interest rate entry from a time deposit product.
        /// </summary>
        [HttpDelete("timedeposits/rates/{rateId:long")]
        public async Task<IActionResult> RemoveTimedepositRate(long rateId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveTimedepositRateCommand(rateId), ct);
            return result.IsSuccess ? Ok(new { Message = "Interest rate removed." })
                : NotFound(new { result.Error });
        }

        /// <summary>
        /// Add an interest tier band for a tiered time deposit product.
        /// Only applicable when TierInterest = true.
        /// </summary>
        [HttpPost("timedeposits/{id:long}/tiers")]
        public async Task<IActionResult> AddTimedepositTier(long id, [FromBody] CreateTimedepositTierRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AddTimedepositTierCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove an interest tier band from a time deposit product.
        /// </summary>
        [HttpDelete("timedeposits/tiers/{tierId:long}")]
        public async Task<IActionResult> RemoveTimedepositTier(long tierId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveTimedepositTierCommand(tierId), ct);
            return result.IsSuccess ? Ok(new { Message = "Interest tier removed." })
                : NotFound(new { result.Error });
        }

        [HttpPut("timedeposits/{id:long}/activate")]
        public async Task<IActionResult> ActivateTimedepositProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateTimedepositProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Time deposit product activated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("timedeposits/{id:long}/deactivate")]
        public async Task<IActionResult> DeactivateTimedepositProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateTimedepositProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Time deposit product deactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("timedeposits/{id:long}")]
        public async Task<IActionResult> DeleteTimedepositProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteTimedepositProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Time deposit product deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion

        #region INSURANCE PRODUCTS

        [HttpGet("insurance")]
        public async Task<IActionResult> GetInsuranceProducts([FromQuery] bool? isActive, CancellationToken ct) {
            var result = await _mediator.Send(new GetInsuranceProductsQuery(isActive), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("insurance/{id:long}")]
        public async Task<IActionResult> GetInsuranceProductById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetInsuranceProductByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost("insurance")]
        [ProducesResponseType(typeof(InsuranceProductDetailDto), 201)]
        public async Task<IActionResult> CreateInsuranceProduct([FromBody] CreateInsuranceProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateInsuranceProductCommand(request), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_CODE"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetInsuranceProductById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("insurance/{id:long}")]
        public async Task<IActionResult> UpdateInsuranceProduct(long id, [FromBody] UpdateInsuranceProductRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateInsuranceProductCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Update insurance product configuration — coverage, premiums, claims, taxes.
        /// </summary>
        [HttpPut("insurance/{id:long}/configuration")]
        public async Task<IActionResult> UpdateInsuranceProductConfig(long id, [FromBody] UpdateInsuranceProductConfigRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateInsuranceProductConfigCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("insurance/{id:long}/activate")]
        public async Task<IActionResult> ActivateInsuranceProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new ActivateInsuranceProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Insurance product activated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPut("insurance/{id:long}/deactivate")]
        public async Task<IActionResult> DeactivateInsuranceProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateInsuranceProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Insurance product deactivated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("insurance/{id:long}")]
        public async Task<IActionResult> DeleteInsuranceProduct(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteInsuranceProductCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Insurance product deleted." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        #endregion
    }
}
