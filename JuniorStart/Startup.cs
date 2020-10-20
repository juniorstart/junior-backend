using FluentValidation.AspNetCore;
using JuniorStart.Configurations;
using JuniorStart.Middlewares;
using JuniorStart.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JuniorStart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration);
            

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddTransient<ApplicationSeed>();
            services.ConfigureFilters();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureServices();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationSeed applicationSeed,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            
            app.ExceptionHandler();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.EnableSwagger();
            app.UseMiddleware<JwtTokenSlidingExpirationMiddleware>();

            ApplicationSeed.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}