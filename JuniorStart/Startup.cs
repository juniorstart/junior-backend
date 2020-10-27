using FluentValidation.AspNetCore;
using JuniorStart.Configurations;
using JuniorStart.Controllers;
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

            services.AddCors();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddTransient<ApplicationSeed>();
            services.ConfigureFilters();
            services.ConfigureAuthentication(Configuration);
            //services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureServices();
            services.AddSignalR();

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
            app.UseCors(options => {
                options.WithOrigins("http://localhost:4200", "https://juniorstartng.herokuapp.com").AllowAnyHeader().AllowAnyMethod().AllowCredentials();

            });
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });
            app.UseMvc();
            app.EnableSwagger();
            app.UseMiddleware<JwtTokenSlidingExpirationMiddleware>();

            ApplicationSeed.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}