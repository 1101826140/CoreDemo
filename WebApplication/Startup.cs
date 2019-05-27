using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens.Saml;
using WebApplication.Dbcontext;
using WebApplication.Filter;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication
{
    public class Startup
    {
        private readonly IConfiguration configuration;


        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = configuration["ConnectionString:CoreDemoDB"].ToString();

            //string connectionString = configuration.GetConnectionString("CoreDemoDB").ToString();
            services.AddDbContext<CoreDBContext>(options =>
            {
                // options.useSqlServer()
                options.UseSqlServer(connectionString);
            });


            services.AddMvc();


            //IWelcomServices 需要注册再使用，否则会出错

            //整个系统只实例化一次
            services.AddSingleton<IWelcomServices, WelcomServices>();

            //每次请求都重写实例化
            //services.AddTransient<IWelcomServices, WelcomServices>();

            //请求期间有其他请求公用该实例
            //services.AddScoped<IWelcomServices, WelcomServices>();
            //每次请求都实例化
            //  services.AddScoped<IRepository<Student>, StudentRepository>();
            //services.AddSingleton<IRepository<Student>, MemoryRepository>();

            services.AddScoped<IRepository<Student>, StudentRepositroy>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.Filters.Add(new LogResourceFilter());
                //options.Filters.Add(typeof(LogAsyncResourceFilter));
                options.Filters.Add<LogResourceFilter>();  //全局过滤器
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configration">
        /// 参数来源：
        /// 1：appsettings.json  如果有appsettings.development.json，则取development文件里面值
        /// 2：系统环境变量       电脑右键属性高级环境变量，控制台运行的时候，不输入参数则读取系统配置的参数
        /// 3：命令行参数         cmd中输入： dotnet run Welcome="come from cmd args, hello world",则加载该文件
        /// 4: user secrets
        /// reshaper 插件可查看源码，但是我这边vs2019似乎不适用
        /// https://www.cnblogs.com/AaronBlogs/p/7041152.html
        /// 优先级： 系统参数>appsettings.development.json>appsettings.json
        /// </param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configration)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.Run(async (context) =>
        //    {
        //        string welcome = configration["Welcome"];
        //        await context.Response.WriteAsync(welcome);
        //    });
        //}


        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IWelcomServices services,
            ILogger<Startup> log
        )
        {
            // app.UseWelcomePage(); //welcome页面
            if (env.IsDevelopment()) //来源于launchSettings.json
            {
                app.UseDeveloperExceptionPage();//开发者异常页面
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/MyError"); //生产环境配置的错误页面
                                                          //app.UseExceptionHandler();

            }

            /*
             //中间件
             
             
            app.Use(next =>
            {
                log.LogInformation("------0");
                return async context =>
                {
                    log.LogInformation("------1");
                    if (context.Request.Path.StartsWithSegments("/first"))
                    {

                        log.LogInformation("------2");
                        await context.Response.WriteAsync("first page");
                    }
                    else
                    {
                        log.LogInformation("------3");
                        await next(context);
                    } 
                };
            });
            log.LogInformation("------4");
            app.UseWelcomePage();
            */
            //app.UseDefaultFiles();//走默认页面
            //app.UseStaticFiles();//使用静态文件，才能访问wwwroot目录下的文件

            //该功能同上两点
            //app.UseFileServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc();


            ////要写个路由
            //app.UseRouter(builder =>
            //{

            //    builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            //});

            //app.UseMvcWithDefaultRoute();
            app.Run(async (context) =>
            {
                // throw new Exception("error by sun");
                string welcome = services.GetMessage();
                await context.Response.WriteAsync(welcome);
            });


        }



    }
}
