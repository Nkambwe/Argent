
namespace Argent.Api.Infrastructure {
    /// <summary>
    /// Marker class used to reference this assembly in MediatR and FluentValidation registrations.
    /// Usage in Program.cs:
    ///   builder.Services.AddMediatR(cfg =>
    ///       cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
    ///   builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
    /// </summary>
    public sealed class AssemblyReference { }
}
