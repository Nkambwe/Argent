
namespace Argent.Api.Infrastructure.Services {
    public interface IOrganizationService: IBaseService {
        Task<bool> CanConnectAsync();
        Task<bool> IsOrganizationSetup();
    }
}
