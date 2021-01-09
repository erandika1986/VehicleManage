using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using VehicleTracker.Business;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Infrastructure.Filters;
using VehicleTracker.WebApi.Helpers;

namespace VehicleTracker.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IVMDBUow, VMDBUow>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IVehicleDifferentialOilChangeMilageService, VehicleDifferentialOilChangeMilageService>();
            services.AddScoped<IVehicleFitnessReportService, VehicleFitnessReportService>();
            services.AddScoped<IVehicleGreeceNipleService, VehicleGreeceNipleService>();
            services.AddScoped<IVehicleInsuranceService, VehicleInsuranceService>();
            services.AddScoped<IVehicleRevenueLicenceService, VehicleRevenueLicenceService>();
            services.AddScoped<IVehicleEmissionTestService, VehicleEmissionTestService>();
            services.AddScoped<IVehicleAirCleanerService, VehicleAirCleanerService>();
            services.AddScoped<IVehicleEngineOilMilageService, VehicleEngineOilMilageService>();
            services.AddScoped<IVehicleFuelFilterMilageService, VehicleFuelFilterMilageService>();
            services.AddScoped<IVehicleGearBoxOilMilageService, VehicleGearBoxOilMilageService>();
            services.AddScoped<IVehicleBeatService, VehicleBeatService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services
                .AddCustomMVC(Configuration)
                .AddCustomDbContext(Configuration)
                .EnableJWTAuthentication(Configuration)
                .AddSwagger();

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {


            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                //loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            IdentityHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "FRS Field Agent API: V1");
                });

            app.UseMvcWithDefaultRoute();
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connStrin = configuration["VehicleDbConnectionString"];

            services.AddDbContext<VMDBContext>(options =>
            {


                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration["VehicleDbConnectionString"],
                                        sqlServerOptionsAction: sqlOptions =>
                                        {
                                            //sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                        });
            });


            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection EnableJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication
                (cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidIssuer = configuration["Tokens:Issuer"],
                   ValidAudiences = new List<string>
                   {
                       "admin"
                   },

                   IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
                   {
                       List<SecurityKey> keys = new List<SecurityKey>();

                       keys.Add(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])));

                       return keys;
                   }
               //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))


           };
           });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "VehicleTracker. - Web API",
                    Version = "v1",
                    Description = "The web service for VehicleTracker",
                    TermsOfService = "Terms Of Service"
                });
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            return services;

        }
    }


}
