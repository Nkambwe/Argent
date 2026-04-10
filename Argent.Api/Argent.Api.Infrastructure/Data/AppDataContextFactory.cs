using Argent.Api.Infrastructure.Cyphers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Argent.Api.Infrastructure.Data {

    public class AppDataContextFactory
    : IDesignTimeDbContextFactory<AppDataContext> {
        public AppDataContext CreateDbContext(string[] args) {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(basePath, "../Argent.Api/appsettings.json"),optional: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionVar = configuration.GetValue<string>(
                "ConnectionOptions:DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionVar))
                throw new Exception(
                    "DB Connection Environment variable name not found in appsettings");

            var encryptedConnection = Environment.GetEnvironmentVariable(connectionVar);

            if (string.IsNullOrWhiteSpace(encryptedConnection))
                throw new Exception(
                    $"Environmental variable '{connectionVar}' not set");

            var decryptedString = HashGenerator.DecryptString(encryptedConnection);

            var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();

            optionsBuilder.UseNpgsql(decryptedString, npgsql => {
                npgsql.MigrationsAssembly(typeof(AppDataContext).Assembly.FullName);
                npgsql.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });

            return new AppDataContext(optionsBuilder.Options);
        }
    }
}
