using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件.MyMiddleware
{
    public class StringContentMiddleware : IMiddleware
    {
        //private string _content;
        //public StringContentMiddleware(string content)
        //{
        //    _content = content;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteLineAsyc("Hello World--StringContentMiddleware");

            // 调用下一个委托
            await next.Invoke(context);
        }
    }
}
