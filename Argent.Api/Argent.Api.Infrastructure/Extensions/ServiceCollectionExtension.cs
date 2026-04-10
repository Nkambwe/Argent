using Argent.Api.Infrastructure.Configuration.Options;
using Argent.Api.Infrastructure.Configuration.Providers;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Cyphers;
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Identity;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Repositories;
using Argent.Api.Infrastructure.Repositories.Access;
using Argent.Api.Infrastructure.Services;
using Argent.Api.Infrastructure.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Argent.Api.Infrastructure.Extensions {

    public static class ServiceCollectionExtension {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            //..get appSettings settings
            services.Configure<EnvironmentOptions>(configuration.GetSection(EnvironmentOptions.SectionName));
            services.Configure<ServiceLoggingOption>(configuration.GetSection(ServiceLoggingOption.SectionName));

            //..register appSettings provider
            services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();
            services.AddScoped<IServiceLoggerFactory, ServiceLoggerFactory>();
            services.AddSingleton<ILoggerProvider, ServiceLoggerProvider>();
            services.AddSingleton<ILoggerConfigurationProvider, LoggerConfigurationProvider>();

            //..database connection
            services.ConfigureDatabaseConnection(configuration);
            
            //..unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

            //..register repositories
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IAccessRepository, AccessRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();

            //..JWT Bearer authentication
            services.ConfigureJWTAuthentication(configuration);
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) {
             services.AddScoped<IOrganizationService, OrganizationService>();
            // services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }

        private static IServiceCollection ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration configuration) {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, HttpContextUserContext>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey must be configured.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

            services.AddAuthorization();

            return services;
        }

        /// <summary>
        /// Database connection configuration
        /// </summary>
        /// <param name="services">Service instance</param>
        private static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration) {

            //..create logger
            using var provider = services.BuildServiceProvider();
            var loggerFactory = provider.GetRequiredService<IServiceLoggerFactory>();
            var _logger = loggerFactory.CreateLogger("mfi");
            _logger.Channel = $"DBCONNECTION-{DateTime.Now:yyyyMMddHHmmss}";
            _logger.Log("Attempting DB Connection...", "Config");

            try {
                //..connection variable name
                var connectionVar = configuration.GetValue<string>("ConnectionOptions:DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionVar)) {
                    string msg = "DB Connection Environment variable name 'MFI_CONNECTION_ENV' not found in appSettings";
                    throw new Exception(msg);
                }

                var isLive = configuration.GetValue<bool>("EnvironmentOptions:IsLive");
                _logger.Log($"ENVIRONMENT ISLIVE >> {isLive}", "Config");

                //..connection string
                var connectionString = Environment.GetEnvironmentVariable(connectionVar);
                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception($"Environmental variable '{connectionVar}' which holds connection string value not set");

                var decryptedString = HashGenerator.DecryptString(connectionString);
                if (isLive) {
                    _logger.Log($"CONNECTION URL :: {connectionString}", "Config");
                } else {
                    _logger.Log($"CONNECTION URL :: {decryptedString}", "Config");
                }

                services.AddDbContext<AppDataContext>(options => {
                    options.UseNpgsql(decryptedString, npgsql => {
                        npgsql.MigrationsAssembly(typeof(AppDataContext).Assembly.FullName);
                        npgsql.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorCodesToAdd: null);
                    });
                });

                _logger.Log("Data Connection Established", "Config");
            } catch (Exception ex) {
                string msg = "Database connection error occurred";
                _logger.Log(msg, "Config");
                _logger.Log($" {ex.Message}", "Db-Error");
                _logger.Log($" {ex.StackTrace}", "STACKTRACE");

                throw new Exception(msg);
            }

            return services;
        }

    }
}
