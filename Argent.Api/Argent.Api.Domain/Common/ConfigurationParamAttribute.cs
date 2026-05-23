namespace Argent.Api.Domain.Common {
    /// <summary>
    /// Marks a property on a product configuration class as a seedable parameter.
    /// Used to drive automatic seeding of ProductParam records when a product is created.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ConfigurationParamAttribute(
        string name,
        string description,
        string paramType = "string") : Attribute {
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string ParamType { get; } = paramType;
    }
}
