using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_日志框架.LogExtensions
{
    public class ExtensionsLoggerProvider : ILoggerProvider
    {
        private readonly ExtensionsConfiguration _config;

        public ExtensionsLoggerProvider(ExtensionsConfiguration extensionsConfiguration)
        {
            _config = extensionsConfiguration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ExtensionsLogger(_config);
        }

        public void Dispose()
        {
        }
    }
}
