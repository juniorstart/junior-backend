using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Filters;
using JuniorStart.Repository;
using JuniorStart.Services;
using JuniorStart.Services.Interfaces;
using JuniorStart.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;

namespace JuniorStart.Configurations
{
    public static class StartupExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<EntityExistsAttribute<User>>();
            services.AddScoped<EntityExistsAttribute<TodoList>>();
            services.AddScoped<EntityExistsAttribute<Task>>();
            services.AddScoped<EntityExistsAttribute<RecruitmentInformation>>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            
            services.RegisterAllTypes(typeof(IModelFactory<,>), typeof(RecruitmentModelFactory),
                ServiceLifetime.Scoped);
            services.AddScoped<IRecruitmentService, RecruitmentService>();
        }

        private static void RegisterAllTypes(this IServiceCollection services, Type baseType, Type sourceType,
            ServiceLifetime lifetime)
        {
            Assembly assembly = sourceType.Assembly;

            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IModelFactory<,>))
                    {
                        // NOTE: Due to a limitation of Microsoft.DependencyInjection we cannot 
                        // register an open generic interface type without also having an open generic 
                        // implementation type. So, we convert to a closed generic interface 
                        // type to register.
                        var interfaceType = typeof(IModelFactory<,>).MakeGenericType(i.GetGenericArguments());
                        services.Add(new ServiceDescriptor(interfaceType, type, lifetime));
                    }
                }
            }
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "JuniorStart API",
                    Description = "Documentation for API",
                    TermsOfService = null
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration.GetSection("JWT").GetSection("SecretKey").Value);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            int userId = int.Parse(context.Principal.Identity.Name);
                            var user = userService.Get(userId);
                            if (user == null)
                            {
                                context.Fail("Unauthorized");
                            }

                            return System.Threading.Tasks.Task.FromResult(0);
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void EnableSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JuniorStart API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void ExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var exception = error.Error;

                        await context.Response.WriteAsync(new ErrorResponse
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = exception.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}