using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件.MyMiddleware
{
    public static class ShowIPExtensions
    {
        public static IApplicationBuilder UseShowIP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ShowIPMiddleware>();
        }
    }
}
