using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _005_Serilog集成
{
    public class Program
    {
        //2、读取配置文件
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

        public static void Main(string[] args)
        {
            // // 1 直接设置输出界别和输出方式
            // Log.Logger = new LoggerConfiguration()
            //.MinimumLevel.Debug()
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //.Enrich.FromLogContext()
            //.WriteTo.Console()
            //.CreateLogger();

            //2、读取配置文件
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(Configuration)
                         .Enrich.FromLogContext()
                         .WriteTo.Debug()   //输出路径
                         .WriteTo.Console( //模板
                         outputTemplate: @"
Timestamp:{Timestamp:HH:mm:ss} 
Level:{Level:u5}
Message:{Message:lj} 
Properties:{Properties:j}
Exception:{Exception}
")    
                         .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                .UseSerilog() // 使用 Serilog
               .ConfigureLogging((hostingContext, logging) =>
               {
                   logging.ClearProviders(); //去掉默认添加的日志提供程序
               })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
    }
}