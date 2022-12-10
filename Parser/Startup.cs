using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parser.Infrastructure;
using Parser.Services;

namespace Parser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //  DefaultHtmlProvider must be the last 
            // services.AddHttpClient<HttpClientFactory>();
            services.AddScoped<IHtmlProvider, AliexpressHtmlProvider>();
            services.AddScoped<IHtmlProvider, DefaultHtmlProvider>();

            services.AddScoped<HttpClientFactory, HttpClientFactory>();
            services.AddScoped<IHtmlParserService, HtmlParserService>();

            services.AddOptions<AliexpressHtmlProviderOptions>()
                .Bind(Configuration.GetSection(AliexpressHtmlProviderOptions.Key)).ValidateDataAnnotations();
            services.AddOptions<DefaultHtmlProviderOptions>()
                .Bind(Configuration.GetSection(DefaultHtmlProviderOptions.Key)).ValidateDataAnnotations();
            services.AddOptions<HttpClientFactoryOptions>()
                .Bind(Configuration.GetSection(HttpClientFactoryOptions.Key)).ValidateDataAnnotations();
            
            services.AddControllers();
            services.AddSwaggerGen();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI();

        }
    }
}
