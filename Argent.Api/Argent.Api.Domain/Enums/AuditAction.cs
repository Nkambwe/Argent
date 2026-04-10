
using System.ComponentModel;

namespace Argent.Api.Domain.Enums {
    public enum AuditAction {
        [Description("Create")]
        Create = 1,
        [Description("Read")]
        Read = 2,
        [Description("Update")]
        Update = 3,
        [Description("Delete")]
        Delete = 4,
        [Description("Login")]
        Login = 5,
        [Description("Logout")]
        Logout = 6,
        [Description("Approve")]
        Approve = 7,
        [Description("Reject")]
        Reject = 8,
        [Description("Reverse")]
        Reverse = 9,
        [Description("Export")]
        Export = 10,
        [Description("Other")]
        Other = 99
    }
}
