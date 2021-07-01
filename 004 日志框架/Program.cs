using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _004_日志框架
{
    public class Program
    {
        #region 日志配置
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        #endregion

        #region 日志作用域
        ///// <summary>
        ///// 日志作用域
        ///// </summary>
        //static void Main()
        //{
        //    var logger = new ServiceCollection()
        //        .AddLogging(builder => builder
        //        .AddConsole(options => options.IncludeScopes = true))
        //        .BuildServiceProvider()
        //        .GetRequiredService<ILogger<Program>>();

        //    using (logger.BeginScope("Foo"))
        //    {
        //        logger.Log(LogLevel.Information, "This is a log written in scope Foo.");
        //        using (logger.BeginScope("Bar"))
        //        {
        //            logger.Log(LogLevel.Information, "This is a log written in scope Bar.");
        //            using (logger.BeginScope("Baz"))
        //            {
        //                logger.Log(LogLevel.Information, "This is a log written in scope Baz.");
        //            }
        //        }
        //    }
        //    Console.Read();
        //} 
        #endregion

        #region LoggerMessage.Define

        //private static Random _random;
        //private static string _template;
        //private static ILogger _logger;
        //private static Action<ILogger, int, long, double, TimeSpan, Exception> _log;

        //static async Task Main()
        //{
        //    _random = new Random();
        //    _template = "Method FoobarAsync is invoked." +
        //        "\n\t\tArguments: foo={foo}, bar={bar}" +
        //        "\n\t\tReturn value: {returnValue}" +
        //        "\n\t\tTime:{time}";
        //    _log = LoggerMessage.Define
        //        <int, long, double, TimeSpan>(LogLevel.Trace, 3721, _template);
        //    _logger = new ServiceCollection()
        //        .AddLogging(builder => builder
        //            .SetMinimumLevel(LogLevel.Trace)
        //            .AddConsole())
        //        .BuildServiceProvider()
        //        .GetRequiredService<ILogger<Program>>();
        //    await FoobarAsync(_random.Next(), _random.Next());
        //    await FoobarAsync(_random.Next(), _random.Next());
        //    Console.Read();
        //}


        //static async Task<double> FoobarAsync(int foo, long bar)
        //{
        //    var stopwatch = Stopwatch.StartNew();
        //    await Task.Delay(_random.Next(100, 900));
        //    var result = _random.Next();
        //    _log(_logger, foo, bar, result, stopwatch.Elapsed, null);
        //    return result;
        //} 

        #endregion

        #region LoggerMessage.DefineScope

        //static async Task Main()
        //{
        //    var logger = new ServiceCollection()
        //            .AddLogging(builder => builder
        //            .SetMinimumLevel(LogLevel.Trace)
        //            .AddConsole(options => options.IncludeScopes = true))
        //        .BuildServiceProvider()
        //        .GetRequiredService<ILogger<Program>>();

        //    var scopeFactory = LoggerMessage.DefineScope<Guid>("Foobar Transaction[{TransactionId}]");
        //    var operationCompleted = LoggerMessage.Define<string, TimeSpan>(LogLevel.Trace, 3721, "Operation {operation} completes at {time}");

        //    using (scopeFactory(logger, Guid.NewGuid()))
        //    {
        //        await InvokeAsync();
        //    }

        //    using (scopeFactory(logger, Guid.NewGuid()))
        //    {
        //        await InvokeAsync();
        //    }

        //    Console.Read();

        //    async Task InvokeAsync()
        //    {
        //        var stopwatch = Stopwatch.StartNew();
        //        await Task.Delay(500);
        //        operationCompleted(logger, "foo", stopwatch.Elapsed, null);

        //        await Task.Delay(300);
        //        operationCompleted(logger, "bar", stopwatch.Elapsed, null);

        //        await Task.Delay(800);
        //        operationCompleted(logger, "baz", stopwatch.Elapsed, null);
        //    }
        //} 
        #endregion
    }
}
