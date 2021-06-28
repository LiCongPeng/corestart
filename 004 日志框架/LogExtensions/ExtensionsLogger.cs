using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_日志框架.LogExtensions
{
    public class ExtensionsLogger : ILogger
    {
        private readonly ExtensionsConfiguration _config;
        public ExtensionsLogger(ExtensionsConfiguration extensionsConfiguration)
        {
            _config = extensionsConfiguration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            Console.WriteLine($" 自定义日志输出： {logLevel} - {eventId.Id} : " + formatter(state, exception));

        }
    }
}