namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class UpdateUserRequest {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public long DefualtBranchId { get; set; }
    }
}
