using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Argent.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(AppDataContext).Assembly.FullName);
                    npgsql.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: null);
                });
            });

            //..unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

            //..register services
            // services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) {
            // services.AddScoped<IOrganizationService, OrganizationService>();
            // services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }

    }
}
