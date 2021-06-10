using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace VehicleTracker.WebApi
{
  public class Program
  {
    public static readonly string Namespace = typeof(Program).Namespace;
    public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);


    //public static void Main(string[] args)
    //{
    //  CreateWebHostBuilder(args).Build().Run();
    //}

    //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //    WebHost.CreateDefaultBuilder(args)
    //        .UseStartup<Startup>();


    public static int Main(string[] args)
    {
      var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables().Build();

      Log.Logger = CreateSerilogLogger(configurationBuilder);

      try
      {
        Log.Information("Configuring web host ({ApplicationContext})...", AppName);
        var host = CreateHostBuilder(configurationBuilder, args);

        Log.Information("Starting web host ({ApplicationContext})...", AppName);
        host.Run();

        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration((builderContext, config) =>
        {
          config.AddConfiguration(configuration);
        })
        //.UseWebRoot("Pics")
        .UseSerilog()
        .Build();

    private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {


      return new LoggerConfiguration()
          .MinimumLevel.Debug()
          .WriteTo.Console()
          .WriteTo.File("logs\\EEGLog.txt", rollingInterval: RollingInterval.Day)
          .CreateLogger();
    }
  }
}
