using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BananaShopNet.Controllers
{
    class BananaTask : IHostedService, IDisposable
    {
        private Timer _timer;
        private IBananaMetrics bananaMetrics;

        public BananaTask(IBananaMetrics inMetrics)
        {
            bananaMetrics = inMetrics;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            bananaMetrics.doStats();
        }


    public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
