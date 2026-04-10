using Argent.Api.Extensions;
using Argent.Api.Infrastructure.Extensions;
using FluentValidation;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Argent.Api
{
    public class Program {
        public static void Main(string[] args)
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

            // Infrastructure (PostgreSQL, EF Core, UoW, Repositories)
            builder.Services.AddInfrastructure(builder.Configuration);

            //..service registration
            builder.Services.ConfigureServices(builder.Configuration);

            //..MediatR (CQRS)
             builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            //..FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            //..CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => 
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
            });

            var app = builder.Build();

            //..middleware pipeline
            app.UseGlobalExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Argent MFI API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //..add built-in probe endpoint, separate from our custom /api/health
            app.MapHealthChecks("/health/ready");
            app.Run();

        }
    }
}



