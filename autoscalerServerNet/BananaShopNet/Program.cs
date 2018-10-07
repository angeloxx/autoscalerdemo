using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BananaShopNet.Controllers;
using Microsoft.AspNetCore.Hosting;
using Prometheus;


namespace BananaShopNet
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMilliseconds(10); })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            var metricServer = new MetricServer(port: 5001);
            metricServer.Start();

            host.Run();
        }
    }
}
