using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using VehicleTracker.Business;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.WebApi.Helpers;
using VehicleTracker.WebApi.Infrastructure.AutofacModules;
using VehicleTracker.WebApi.Infrastructure.Filters;

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
      services
        .AddCustomMVC(Configuration)
        .AddCustomDbContext(Configuration)
        .EnableJWTAuthentication(Configuration)
        .EnableMultiPartBody(Configuration)
        .AddSwagger();


      var container = new ContainerBuilder();
      container.Populate(services);

      container.RegisterModule(new ApplicationModule());

      return new AutofacServiceProvider(container.Build());

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {


      var pathBase = Configuration["PATH_BASE"];
      if (!string.IsNullOrEmpty(pathBase))
      {
        loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
        app.UsePathBase(pathBase);
      }

      app.UseSwagger()
       .UseSwaggerUI(c =>
       {
         c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Catalog.API V1");
       });


      app.UseCors("CorsPolicy");
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
      });
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

      var allowedOrigins = new List<string>();
      var allowOrigins = configuration["AllowedOrigins"].Split(",");

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
                  builder => builder.WithOrigins(allowOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
      });

      return services;
    }

    public static IServiceCollection EnableJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

      // services.AddAuthentication
      //     (cfg =>
      //     {
      //       cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //       cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //     })
      //.AddJwtBearer(options =>
      //{
      //  options.TokenValidationParameters = new TokenValidationParameters
      //  {
      //    ValidateIssuer = true,
      //    ValidateAudience = true,
      //    ValidateLifetime = true,
      //    ValidateIssuerSigningKey = true,

      //    ValidIssuer = configuration["Tokens:Issuer"],
      //    ValidAudiences = new List<string>
      //        {
      //                  "admin"
      //        },

      //    IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
      //          {
      //          List<SecurityKey> keys = new List<SecurityKey>();

      //          keys.Add(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])));

      //          return keys;
      //        }
      //              //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))


      //        };
      //});

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.RequireHttpsMetadata = false;
  options.SaveToken = true;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = configuration["Tokens:Issuer"],
    ValidAudiences = new List<string>
        {
                          "admin","webapp"
        },

    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
    ClockSkew = TimeSpan.Zero
  };
});

      return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(options =>
      {
        //options.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "EEG - Web API",
          Version = "v1",
          Description = "The web service for EEG",
          TermsOfService = new Uri("https://example.com/terms")
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          //BearerFormat = "JWT",
          //Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
      });

      return services;

    }

    public static IServiceCollection EnableMultiPartBody(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<FormOptions>(o =>
      {
        o.ValueLengthLimit = int.MaxValue;
        o.MultipartBodyLengthLimit = long.MaxValue;
        o.MemoryBufferThreshold = int.MaxValue;
        o.ValueCountLimit = int.MaxValue;
        o.MemoryBufferThreshold = int.MaxValue;
      });

      services.AddMvc(options =>
      {
        options.MaxModelBindingCollectionSize = int.MaxValue;
      });

      return services;
    }

  }


}
