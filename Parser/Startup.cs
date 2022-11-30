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
            services.AddScoped<IHtmlProvider, AliexpressHtmlProvider>();
            services.AddScoped<IHtmlProvider, DefaultHtmlProvider>();
            
            services.AddScoped<IHtmlParserService, HtmlParserService>();
            
            services.Configure<DefaultHtmlProviderOptions>(Configuration.GetSection(DefaultHtmlProviderOptions.Key));
            services.Configure<AliexpressHtmlProviderOptions>(Configuration.GetSection(AliexpressHtmlProviderOptions.Key));
            
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
