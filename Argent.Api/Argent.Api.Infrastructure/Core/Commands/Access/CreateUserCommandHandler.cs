using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class CreateUserCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<CreateUserCommand, Result<UserDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");

            var req = command.Request;

            logger.Channel = $"CREATE-USER-{req.Username.ToUpper()}";
            logger.Log($"Creating user: {req.Username}", "INFO");

            //..validate uniqueness
            if (await _uow.Users.UsernameExistsAsync(req.Username, token:ct))
                return Result<UserDto>.Failure($"Username '{req.Username}' is already taken.", "DUPLICATE_USERNAME");

            if (await _uow.Users.EmailExistsAsync(req.Email, token:ct))
                return Result<UserDto>.Failure($"Email '{req.Email}' is already registered.", "DUPLICATE_EMAIL");

            // Verify home branch exists
            var branch = await _uow.Organizations.GetBranchByIdAsync(req.DefualtBranchId, ct);
            if (branch is null) {
                logger.Log($"Not Found!: Default branch not found", "INFO");
                return Result<UserDto>.NotFound("Default branch not found.");
            }
            
            //..validate all roles exist before starting transaction
            foreach (var roleId in req.RoleIds) {
                var role = await _uow.Roles.GetByIdAsync(roleId, ct);
                if (role is null)
                    return Result<UserDto>.NotFound($"Role {roleId} not found.");
            }

            var result = await _uow.ExecuteInTransactionAsync(async token => {
                // ..create user
                var user = new AppUser
                {
                    Username = req.Username,
                    Email = req.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password, workFactor: 12),
                    FirstName = req.FirstName,
                    MiddleName = req.MiddleName,
                    LastName = req.LastName,
                    PhoneNumber = req.PhoneNumber,
                    DefaultBranchId = req.DefualtBranchId,
                    IsActive = true
                };

                await _uow.Users.AddAsync(user, token);
                //..first SaveChanges to get user.Id
                await _uow.CommitAuditAsync(token); 

                //..assign roles
                foreach (var roleId in req.RoleIds)
                    await _uow.Users.AssignRoleToUserAsync(user.Id, roleId, token);

                //..default branch access is always granted
                await _uow.Users.AssignBranchAccessAsync(user.Id, req.DefualtBranchId, canPost: true, token);

                //..grant additional branch access
                foreach (var ba in req.AdditionalBranchAccess
                    .Where(ba => ba.BranchId != req.DefualtBranchId)) {
                    await _uow.Users.AddBranchAccessAsync(new UserBranchAccess {
                        UserId = user.Id,
                        BranchId = ba.BranchId,
                        CanPost = ba.CanPost
                    }, token);
                }

                //..second SaveChanges for roles and branch access
                await _uow.CommitAuditAsync(token); 

                logger.Log($"User created: {user.Username} (Id: {user.Id})", "INFO");

                //..reload roles for response
                var roles = await _uow.Roles.GetAllAsync(token);
                var assignedRoleNames = roles.Where(r => req.RoleIds.Contains(r.Id)).Select(r => r.Name);

                //..record password history
                await _uow.Users.AddPasswordHistoryAsync(new PasswordHistory
                {
                    UserId = user.Id,
                    PasswordHash = user.PasswordHash,
                    ChangedOn = DateTime.UtcNow,
                    CreatedBy = "system"
                }, token);

                logger.Log($"User created: {user.Username} ({user.FirstName} {user.LastName})", "ACCESS-OK");

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    DefaultBranchId = user.DefaultBranchId,
                    DefaultBranchCode = branch?.BranchCode ?? "",
                    DefaultBranchName = branch?.BranchName ?? "",
                    IsActive = user.IsActive,
                    CreatedOn = user.CreatedOn,
                    Roles = assignedRoleNames,
                    BranchAccess = [new BranchAccessDto
                    {
                        BranchId   = branch?.Id ?? 1,
                        BranchCode = branch?.BranchCode ?? "",
                        BranchName = branch?.BranchName ?? "",
                        CanPost    = true
                    }]
                };
            }, ct);

            return Result<UserDto>.Success(result);

        }
    }
}
