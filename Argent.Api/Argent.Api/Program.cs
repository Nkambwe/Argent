using Argent.Api.Extensions;
using Argent.Api.Infrastructure.Core.Common.Behaviour;
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Extensions;
using FluentValidation;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Argent.Api
{
    public class Program {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory,
                ApplicationName = Process.GetCurrentProcess().ProcessName
            });

            //..services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(t => t.FullName);
                options.SwaggerDoc("v1", new()
                {
                    Title = "Argent API",
                    Version = "v1",
                    Description = "Core API for the Argent MFI management platform"
                });

            });

            //..enable logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // register ASP.NET built-in health checks
            builder.Services.AddHealthChecks();

            //..FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            // Infrastructure
            builder.Services.AddInfrastructure(builder.Configuration);

            //..service registration
            builder.Services.ConfigureServices(builder.Configuration);

            //..MediatR ordered pipeline behaviours
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Infrastructure.AssemblyReference).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(BranchPolicyBehavior<,>));
                cfg.AddOpenBehavior(typeof(AuditPipelineBehavior<,>));
            });

            //..CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => 
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
            });

            var app = builder.Build();

            //..seed permissions and default roles
            // Safe to run every startup — idempotent, skips existing records
            await DatabaseSeeder.SeedAsync(app.Services);

            //..middleware pipeline
            app.UseGlobalExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Argent MFI API v1");
                    c.RoutePrefix = "swagger"; // set c.RoutePrefix = string.Empty; for Production
                });
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //..add built-in probe endpoint, separate from our custom /api/health
            app.MapHealthChecks("/health/ready");
            app.Run();

        }
    }
}



