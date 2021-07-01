using _003_中间件.MyMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _003_中间件
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSingleton(new StringContentMiddleware());
            services.AddSingleton(new RefusalMiddleware());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseMiddleware<RefusalMiddleware>();

            #region 默认中间件

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            #endregion

            #region Run

            //// 终端中间件，后面的中间件不会再执行了
            //// Run方法向应用程序的请求管道中添加一个RequestDelegate委托
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("app  Run 1 \r\n");
            //});

            //// 这个输出不会执行到
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("app  Run 2 \r\n");
            //});

            #endregion

            #region Use  (Func<RequestDelegate, RequestDelegate> middleware)

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 1");
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 1 Start \r\n");

            //        await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 1 End \r\n");

            //    };
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 2 Start \r\n");

            //        await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 2 End \r\n");

            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 3");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");

            //        //await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 3 End \r\n");

            //    });
            //});

            #endregion

            #region Use next 【短路】

            //// 添加一个Func 委托，表示一个中间件
            //// context 表示HTTP的上下文对象，HTTPContext
            //// next参数表示管道中的下一个中间件委托,如果不调用next，则会使管道短路
            //// 用Use可以将多个中间件链接在一起
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("before app Use 1 \r\n");
            //    // 调用下一个委托
            //    await next();

            //    await context.Response.WriteAsync("after app Use 1 \r\n");

            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("before app Use 2 \r\n");
            //    // 调用下一个委托
            //    await next();

            //    await context.Response.WriteAsync("after app Use 2 \r\n");

            //});

            //// 没有调用 next  【短路】
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("app Use 3 \r\n");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("before app Use 4 \r\n");

            //    // 调用下一个委托
            //    await next();
            //    await context.Response.WriteAsync("after app Use 4\r\n");

            //});

            #endregion

            #region Map

            //// 一个匹配
            //app.Map("/Map1", app1 =>
            //{
            //    app1.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Map1 \r\n");
            //    });
            //});

            //// 支持嵌套
            //app.Map("/Map2", app2 =>
            //{
            //    app2.Map("/Map3", app3 =>
            //        app3.Run(async context =>
            //        {
            //            await context.Response.WriteAsync("/Map2/Map3 \r\n");
            //        }));

            //    // 没有匹配上
            //    app2.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("no map \r\n");
            //    });
            //});

            //// 同时匹配多个段
            //app.Map("/Map1/Map5", app4 =>
            //{
            //    app4.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("/Map4/Map5 \r\n");
            //    });
            //});

            #endregion

            #region Mapwhen

            //// Func委托，输入参数是HttpContext，返回bool值    
            //app.MapWhen(context =>
            //        {
            //            // 判断url参数中是否包含name
            //            return context.Request.Query.ContainsKey("name");
            //        },
            //        // 满足条件后执行的内容
            //        app2 => app2.Run(async context =>
            //            {
            //                await context.Response.WriteAsync($"my name is {context.Request.Query["name"]}");
            //            }));

            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("age");
            //},
            //       app2 => app2.Run(async context =>
            //       {
            //           await context.Response.WriteAsync($"my age is {context.Request.Query["age"]}");
            //       }));

            #endregion

            #region 自定义中间件

            //app.UseMiddleware<StringContentMiddleware>();

            //app.UseShowIP();

            ////为什么不能单独使用，后面必须要终结点?
            //app.Run(async context =>
            //{
            //    await context.Response.WriteLineAsyc("end");
            //});

            #endregion

            #region 中间件源码解析

            // 原始注册内容
            /* Use 方法
            private readonly IList<Func<RequestDelegate, RequestDelegate>> _components = new List<Func<RequestDelegate, RequestDelegate>>();

            public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
            {
                _components.Add(middleware);
                return this;
            }
            */

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 1");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 1 Start \r\n");

            //        await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 1 End \r\n");

            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 2 Start \r\n");

            //        await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 2 End \r\n");

            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 3");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");

            //        //await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 3 End \r\n");

            //    });
            //});

            //// Build 之后内容

            ///* Build 方法
            //public RequestDelegate Build()
            //{
            //    RequestDelegate app = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status404NotFound;
            //        return Task.CompletedTask;
            //    };

            //    foreach (var component in _components.Reverse())
            //    {
            //        app = component(app);
            //    }

            //    return app;
            //}
            //*/

            //RequestDelegate app2 = new RequestDelegate(async context =>
            //{
            //    await context.Response.WriteAsync("app Use Middleware 1 Start \r\n");

            //    {
            //        await context.Response.WriteAsync("app Use Middleware 2 Start \r\n");

            //        {
            //            await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");


            //            await context.Response.WriteAsync("app Use Middleware 3 End \r\n");
            //        }

            //        await context.Response.WriteAsync("app Use Middleware 2 End \r\n");
            //    }

            //    await context.Response.WriteAsync("app Use Middleware 1 End \r\n");

            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 1");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 1 Start \r\n");

            //        //await next.Invoke(context);
            //        {
            //            await context.Response.WriteAsync("app Use Middleware 2 Start \r\n");

            //            //await next.Invoke(context);
            //            {
            //                await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");

            //                //await next.Invoke(context);

            //                await context.Response.WriteAsync("app Use Middleware 3 End \r\n");
            //            }

            //            await context.Response.WriteAsync("app Use Middleware 2 End \r\n");
            //        }

            //        await context.Response.WriteAsync("app Use Middleware 1 End \r\n");

            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 2 Start \r\n");

            //        //await next.Invoke(context);
            //        {
            //            await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");

            //            //await next.Invoke(context);

            //            await context.Response.WriteAsync("app Use Middleware 3 End \r\n");
            //        }

            //        await context.Response.WriteAsync("app Use Middleware 2 End \r\n");

            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 3");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("app Use Middleware 3 Start \r\n");

            //        //await next.Invoke(context);

            //        await context.Response.WriteAsync("app Use Middleware 3 End \r\n");

            //    });
            //});

            #endregion


        }
    }
}
