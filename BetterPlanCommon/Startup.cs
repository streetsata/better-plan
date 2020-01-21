using System;
using BetterPlan.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using BetterPlanCommon.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

namespace BetterPlan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMSSqlContext(Configuration);
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.AddControllers();
            services.ConfigureSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            var swaggerConfig = Configuration.GetSection("SwaggerOptions");
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.RoutePrefix = "docs";
                option.SwaggerEndpoint(swaggerConfig.GetSection("UIEndpoint").Value, swaggerConfig.GetSection("Description").Value);
                option.InjectStylesheet("/docs-ui/custom.css");
                option.InjectJavascript("/docs-ui/custom.js");
                option.DocumentTitle = "BetterPlan Api Docs";
            });

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
