using _003_中间件.MyMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .Build().Run();

            // Host.CreateDefaultBuilder()
            //.ConfigureServices(service => service.AddSingleton(new StringContentMiddleware()))
            //.ConfigureWebHostDefaults(builder =>
            //{
            //    builder.Configure(app => app.UseMiddleware<StringContentMiddleware>());
            //}).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            ;
    }
}
