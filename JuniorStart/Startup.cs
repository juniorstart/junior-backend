using System.Text;
using FluentValidation.AspNetCore;
using JuniorStart.Configurations;
using JuniorStart.Repository;
using JuniorStart.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

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

            services.AddMvc(config => { config.RespectBrowserAcceptHeader = true; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddTransient<ApplicationSeed>();

            services.ConfigureFilters();
            
            services.ConfigureAuthentication(Configuration);

            services.ConfigureSwagger();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationSeed applicationSeed)
        {
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
            app.UseMvc();

            app.EnableSwagger();

            ApplicationSeed.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}