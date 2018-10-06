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
            IBananaMetrics bananaMetrics;
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            var metricServer = new MetricServer(port: 5001);
            metricServer.Start();

            host.Run();
        }
    }
}
