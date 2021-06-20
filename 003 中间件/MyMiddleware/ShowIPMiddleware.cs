using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件.MyMiddleware
{
    public class ShowIPMiddleware
    {
        // 私有字段
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="next"></param>
        public ShowIPMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"This is IP : {context.Connection.RemoteIpAddress.ToString() } \r\n");

            // 调用下一个委托
            await _next.Invoke(context);
        }
    }
}
