using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Argent.Api.Helpers {
    public class SecurityRequirementsDocumentFilter : IDocumentFilter {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) {
            // Add the security requirement directly to the document
            var securitySchemeReference = new OpenApiSecuritySchemeReference("Bearer", swaggerDoc);

            swaggerDoc.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [securitySchemeReference] = new List<string>()
                }
            };
        }
    }
}
