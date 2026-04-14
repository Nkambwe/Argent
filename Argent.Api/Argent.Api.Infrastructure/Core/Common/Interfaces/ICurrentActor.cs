namespace Argent.Api.Infrastructure.Core.Common.Interfaces {
    public interface ICurrentActor {
        string Username { get; }
        bool IsAuthenticated { get; }
    }
}
