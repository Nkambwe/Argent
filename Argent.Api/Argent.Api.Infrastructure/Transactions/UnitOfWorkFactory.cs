using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argent.Api.Infrastructure.Transactions {
    public class UnitOfWorkFactory(IServiceProvider serviceProvider) : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IUnitOfWork Create()
        {
            //..creates a fresh DbContext and Uow per call, caller is responsible for disposing
            var optionsAccessor = _serviceProvider.GetRequiredService<DbContextOptions<AppDataContext>>();
            var context = new AppDataContext(optionsAccessor);
            return new UnitOfWork(context);
        }
    }

}
