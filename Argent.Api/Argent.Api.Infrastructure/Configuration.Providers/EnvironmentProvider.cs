using Argent.Api.Infrastructure.Configuration.Options;
using Microsoft.Extensions.Options;

namespace Argent.Api.Infrastructure.Configuration.Providers {
    public class EnvironmentProvider(IOptions<EnvironmentOptions> options) : IEnvironmentProvider {

        private readonly EnvironmentOptions _options = options.Value;
        public bool IsLive => _options.IsLive;
       

    }
}
