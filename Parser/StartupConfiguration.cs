using Microsoft.Extensions.DependencyInjection;
using Parser.Infrastructure;
using Parser.Services;
using System.Runtime.CompilerServices;

namespace Parser
{
    public static class StartupConfiguration
    {
        public static void injectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDataProviderAsync, Requester>();
            services.AddScoped<IParser, HTMLParser>();
        }
    }
}
