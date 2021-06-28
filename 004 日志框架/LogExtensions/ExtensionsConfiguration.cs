using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_日志框架.LogExtensions
{
    public class ExtensionsConfiguration
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    }
}
