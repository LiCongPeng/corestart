using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件.MyMiddleware
{
    public class RefusalMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var url = context.Request.Path.Value;

            if (url.Contains(".png") == false)
            {
                await next(context);
            }
            else
            {
                string urlReferer = context.Request.Headers["Referer"];

                // 直接访问
                if (string.IsNullOrWhiteSpace(urlReferer))
                {
                    await SetForbiddenImage(context);//返回404图片
                }
                else if (!urlReferer.Contains("localhost"))//非当前域名
                {
                    await SetForbiddenImage(context);//返回404图片
                }
                else
                {
                    // 调用下一个委托
                    await next.Invoke(context);
                }
            }
        }

        /// <summary>
        /// 设置拒绝图片
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task SetForbiddenImage(HttpContext context)
        {
            string defaultImagePath = "wwwroot/image/404.png";
            string path = Path.Combine(Directory.GetCurrentDirectory(), defaultImagePath);

            FileStream fs = File.OpenRead(path);
            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, bytes.Length);
            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
