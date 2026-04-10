using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;

namespace Argent.Api.Infrastructure.Services {
    public class OrganizationService(IServiceLoggerFactory loggerFactory,
        IUnitOfWorkFactory uowFactory) : BaseService(loggerFactory, uowFactory),
        IOrganizationService {
        public async Task<bool> CanConnectAsync() {
            using var uow = UowFactory.Create();
            Logger.Log("HEAL-CHECKS:: CHECK DATABASE HEALTH", "INFO");

            try {
                return await uow.Organizations.CanConnectAsync();
            } catch (Exception ex) {
                Logger.Log($"UNKNOWN STATUS: {ex.Message}", "ERROR");
                throw;
            }
        }

        public async Task<bool> IsOrganizationSetup() {
            using var uow = UowFactory.Create();
            Logger.Log("HEAL-CHECKS:: CHECK IF CAN CONNECT", "INFO");

            try {
                return await uow.Organizations.IsInitlialized();
            }
            catch (Exception ex) {
                Logger.Log($"UNKNOWN STATUS: {ex.Message}", "ERROR");
                throw;
            }
        }
    }

}
