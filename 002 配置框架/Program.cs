using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace 配置框架
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineConfigurationSourceDemo(args);

            Console.ReadKey();
        }

        private static void MemoryConfigurationSourceDemo()
        {
            var source = new Dictionary<string, string>
            {
                ["DataFormat"] = "yyyy dd mm",
                ["format:TimeFormat"] = "yyyy dd mm hh MM",

            };

            var config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            //var obj = config.GetSection("format").Get<FormatOption>();

            Console.WriteLine(config["DataFormat"]);

            Console.WriteLine(config.GetSection("format")["TimeFormat"]);

        }

        class FormatOption
        {

        }

        private static void JsonConfigurationSourceDemo()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsetting.json", optional: true, reloadOnChange: true).Build();

            // 监听JSON文件的变更
            // 一次性的 监听
            var changeToken = config.GetReloadToken();
            changeToken.RegisterChangeCallback(state =>
            {
                Console.WriteLine(config["DataFormat"]);
            }, config);

            // 订阅变更事件
            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                Console.WriteLine(config["DataFormat"]);
                //Console.WriteLine(config.GetValue<string>("DataFormat"));
            });
        }

        private static void EnvironmentSourceDemo()
        {
            // 设置环境变量  
            Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
            Environment.SetEnvironmentVariable("TEST_AGE", "Male");

            Environment.SetEnvironmentVariable("Name", "Male");

            // 读取环境变量的值，以Test_ 开头的，只会读取到前两个
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("TEST_")
                .Build();

            Console.WriteLine(config["GENDER"]);

        }

        private static void CommandLineConfigurationSourceDemo(string[] arg)
        {
            var map = new Dictionary<string, string>
            {
                ["-a"] = "argment"
            };

            var config = new ConfigurationBuilder()
                .AddCommandLine(arg, map).
                Build();

            Console.WriteLine("这是我从命令行读取的参数："+config["argment"]);
        }

    }
}
