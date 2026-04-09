using Argent.Api.Extensions;
using Argent.Api.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//..services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Sacco API",
        Version = "v1",
        Description = "Core API for the Sacco management platform"
    });
});

// Infrastructure (PostgreSQL, EF Core, UoW, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// ── Module service registrations go here as modules are built ──────────────
// Example:
// builder.Services.AddScoped<IOrganizationService, OrganizationService>();
// builder.Services.AddScoped<ICustomerService, CustomerService>();

// ── MediatR (CQRS) — uncomment when you add your first Command/Query ───────
// builder.Services.AddMediatR(cfg =>
//     cfg.RegisterServicesFromAssembly(typeof(SaccoApi.Application.AssemblyReference).Assembly));

// ── FluentValidation — uncomment when validators are added ─────────────────
// builder.Services.AddValidatorsFromAssembly(
//     typeof(SaccoApi.Application.AssemblyReference).Assembly);

// CORS — tighten before production
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAll", policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

//..middleware pipeline

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sacco API v1");
c.RoutePrefix = string.Empty; // Swagger UI at root in dev
});
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//..add built-in probe endpoint, separate from our custom /api/health
app.MapHealthChecks("/health/ready");

app.Run();

// Expose Program for integration test projects
public partial class Program { }
