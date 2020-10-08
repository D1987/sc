using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace Server.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
#if LINUX
            //.UseKestrel(x => x.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30))
            //.UseContentRoot(Directory.GetCurrentDirectory())
#else
            .UseKestrel(x => x.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30))
            .UseContentRoot(Directory.GetCurrentDirectory())
#endif
            .ConfigureAppConfiguration((hostingContext, config) => {
                var env = hostingContext.HostingEnvironment;
                config.SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            .Build();
    }
}
