using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:9000")
                .ConfigureAppConfiguration((hostingContext, builder) => { builder.AddJsonFile("configuration.json", false, true); })
                .Build();
        }
    }


    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        //CreateWebHostBuilder(args).Run();
    //        IWebHostBuilder builder = new WebHostBuilder();
    //        builder.ConfigureServices(s =>
    //        {
    //            s.AddSingleton(builder);
    //        });
    //        builder.UseKestrel()
    //               .UseContentRoot(Directory.GetCurrentDirectory())
    //               .UseStartup<Startup>()
    //               .UseUrls("http://localhost:9000");

    //        var host = builder.Build();
    //        host.Run();
    //    }

    //    public static IWebHost CreateWebHostBuilder(string[] args)
    //    {
    //      return  WebHost.CreateDefaultBuilder(args)
    //            .UseStartup<Startup>()
    //            .UseUrls($"http://localhost:55954")
    //            .ConfigureAppConfiguration((hostingContext,builder)=>
    //            {
    //                builder.AddJsonFile("configuration.json", false, true);
    //            })
    //            .Build();
    //    }

    //}
}
