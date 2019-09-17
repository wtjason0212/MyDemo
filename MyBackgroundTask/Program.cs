using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApplication1.Tasks;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                var host = CreateWebHostBuilder(configuration,args);

                host.Run();
                //return 0;
            }
            catch( Exception ex)
            {
                //return 1;
            }
          

            //using (var cancelSource = new CancellationTokenSource())
            //{
            //    try
            //    {
            //        await new HostBuilder()
            //        .ConfigureServices((hostContext, services) =>
            //        {
            //            services.AddHostedService<TestBackgroundTask>();
            //        }).Build()
            //        .RunAsync();
            //    }
            //    catch (Exception E)
            //    {
            //        cancelSource.Cancel();
            //    }
            //}
        }
 

        public static IWebHost CreateWebHostBuilder(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();
        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var seq = configuration["Serilog:SeqServerUrl"];
            return new LoggerConfiguration()
                //.ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            return builder.Build();
        }
    }
}
