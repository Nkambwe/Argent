using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class UsersController(IMediator mediator, IUnitOfWork uow, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        /// <summary>
        /// List all active users across the organization.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken ct) {
            var users = await _uow.Users.GetAllAsync(ct);
            var dtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                DefaultBranchId = u.DefaultBranchId,
                DefaultBranchCode = u.DefaultBranch?.BranchCode ?? string.Empty,
                DefaultBranchName = u.DefaultBranch?.BranchName ?? string.Empty,
                IsActive = u.IsActive,
                LastLoginOn = u.LastLoginOn,
                CreatedOn = u.CreatedOn
            });

            return Ok(dtos);
        }

        /// <summary>
        /// Get a user by ID including their roles and branch access.
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var user = await _uow.Users.GetByIdWithAccessAsync(id, ct);
            if (user is null)
                return NotFound(new { Error = "User not found." });

            var permissions = await _uow.Permissions.GetUserPermissionsAsync(id, ct);
            var branchAccess = await _uow.Users.GetBranchAccessAsync(id, ct);

            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                DefaultBranchId = user.DefaultBranchId,
                DefaultBranchCode = user.DefaultBranch?.BranchCode ?? string.Empty,
                DefaultBranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                IsActive = user.IsActive,
                LastLoginOn = user.LastLoginOn,
                CreatedOn = user.CreatedOn,
                Roles = user.UserRoles
                    .Where(ur => !ur.IsDeleted)
                    .Select(ur => ur.Role.Name),
                BranchAccess = branchAccess.Select(ba => new BranchAccessDto
                {
                    BranchId = ba.BranchId,
                    BranchCode = ba.Branch?.BranchCode ?? string.Empty,
                    BranchName = ba.Branch?.BranchName ?? string.Empty,
                    CanPost = ba.CanPost
                })
            });
        }

        /// <summary>
        /// Create a new system user. Requires Access.CreateUser permission.
        /// The user is automatically granted access to their home branch.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateUserCommand(request), ct);

            if (!result.IsSuccess) {
                return result.ErrorCode switch
                {
                    "DUPLICATE_USERNAME" or "DUPLICATE_EMAIL" => Conflict(new { result.Error }),
                    "NOT_FOUND" => NotFound(new { result.Error }),
                    _ => BadRequest(new { result.Error })
                };
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Change the calling user's own password.
        /// </summary>
        [HttpPatch("me/change-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken ct) {
            if (!_userContext.IsAuthenticated)
                return Unauthorized();

            if (request.NewPassword != request.ConfirmNewPassword)
                return BadRequest(new { Error = "New password and confirmation do not match." });

            var result = await _mediator.Send(new ChangePasswordCommand(_userContext.UserId, request), ct);
            return result.IsSuccess ? Ok(new { Message = "Password changed successfully." }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Grant a user access to an additional branch.
        /// </summary>
        [HttpPost("{userId:long}/branch-access")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GrantBranchAccess(long userId, [FromBody] GrantBranchAccessRequest request, CancellationToken token) {
            var user = await _uow.Users.GetByIdAsync(userId, token);
            if (user is null)
                return NotFound(new { Error = "User not found." });

            var branch = await _uow.Organizations.GetBranchByIdAsync(request.BranchId, token);
            if (branch is null)
                return NotFound(new { Error = "Branch not found." });

            await _uow.Users.AssignBranchAccessAsync(userId, request.BranchId, request.CanPost, token);
            await _uow.CommitAsync(token);

            return Ok(new {
                Message = $"Access to '{branch.BranchName}' granted to '{user.Username}'.",
                BranchId = branch.Id,
                branch.BranchCode,
                branch.BranchName,
                request.CanPost
            });
        }

        /// <summary>
        /// List all roles with their permissions.
        /// </summary>
        [HttpGet("/api/roles")]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        public async Task<IActionResult> GetRoles(CancellationToken token) {
            var roles = await _uow.Roles.GetAllAsync(token);
            var dtos = new List<RoleDto>();

            foreach (var role in roles) {
                var full = await _uow.Roles.GetByIdAsync(role.Id, token);
                dtos.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    IsSystemRole = role.IsSystemRole,
                    Permissions = [.. full!.RolePermissions.Where(rp => !rp.IsDeleted).Select(rp => new PermissionDto
                    {
                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name,
                        Module = rp.Permission.Module,
                        Action = rp.Permission.Action,
                        Description = rp.Permission.Description
                    })]
                });
            }

            return Ok(dtos);
        }

        /// <summary>
        /// List all available permissions grouped by module.
        /// </summary>
        [HttpGet("/api/permissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionDto>), 200)]
        public async Task<IActionResult> GetPermissions(CancellationToken ct) {
            var permissions = await _uow.Permissions.GetAllAsync(ct);
            var dtos = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Module = p.Module,
                Action = p.Action,
                Description = p.Description
            });

            return Ok(dtos);
        }
    }
}
