using Microsoft.Extensions.DependencyInjection;
using Parser.Infrastructure;
using Parser.Services;

namespace Parser
{
    public static class StartupConfiguration
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IHtmlProvider, AliexpressHtmlProvider>();
            services.AddScoped<IHtmlProvider, DefaultHtmlProvider>();
            services.AddScoped<IParserService, HtmlParserService>();
        }
    }
}
