using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//using Serilog;
//using Serilog.Events;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //第三方Serilog包

           //new LoggerConfiguration().MinimumLevel.Debug()
           //     .MinimumLevel.Override("Mirosoft", LogEventLevel.Information)
           //     .Enrich.FromLogContext()
                
           //     .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureLogging((hostingContext, logging) =>
               {

                   //读取配置
                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                   logging.AddConsole();
               })
                .UseStartup<Startup>();
    }
}
