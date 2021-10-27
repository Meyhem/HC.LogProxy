using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HC.LogProxy.Configuration;
using HC.LogProxy.Core;
using HC.LogProxy.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            
            services.AddControllers();
            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddHttpClient("AirTableClient", (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<LogProxyOptions>();
                client.BaseAddress = options.LogServerUri;
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", options.LogServerApiKey);
            });

            services.AddLogProxy();
            services.AddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HC.LogProxy.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HC.LogProxy.Api v1"));
            }

            app.UseHttpsRedirection();
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