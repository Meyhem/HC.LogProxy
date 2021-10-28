using System;
using System.IO;
using System.Net.Http.Headers;
using HC.LogProxy.Api.Instrumentation;
using HC.LogProxy.Configuration;
using HC.LogProxy.Core;
using HC.LogProxy.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace HC.LogProxy.Api
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
            ConfigureOptionsFor<LogProxyOptions>(services, "LogProxy");
            
            services.AddControllers(opt => opt.Filters.Add<ExceptionInterceptor>());
            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddHttpClient("AirTableClient", (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<LogProxyOptions>>();
                client.BaseAddress = options.Value.LogServerUri;
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", options.Value.LogServerApiKey);
            });

            services.AddLogProxy();
            services.AddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
            services.AddTransient<IIdentifierProvider, IdentifierProvider>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HC.LogProxy.Api", Version = "v1"});
                c.IncludeXmlComments(Path.Join(AppContext.BaseDirectory, "HC.LogProxy.Api.xml"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HC.LogProxy.Api v1"));

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private TOptions ConfigureOptionsFor<TOptions>(IServiceCollection collection, string key) where TOptions : class
        {
            var optionsSection = Configuration.GetSection(key);
            collection.Configure<TOptions>(optionsSection);
            var options = optionsSection.Get<TOptions>();

            return options;
        }
    }
}