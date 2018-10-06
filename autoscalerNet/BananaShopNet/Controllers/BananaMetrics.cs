using System;
using System.Collections.Generic;
using System.Text;
using Prometheus;

namespace BananaShopNet.Controllers
{
    public interface IBananaMetrics
    {
        void addBanana();
        int getBananas();
    }
    public sealed class BananaMetrics : IBananaMetrics
    {
        private static readonly BananaMetrics _instance = new BananaMetrics();
        private Prometheus.Gauge BananaMetric = Metrics.CreateGauge("win_banana_metrics_counter", "dot.net bananas");
        private int Bananas = 0;

        public static BananaMetrics Instance
        {
            get
            {
                return _instance;
            }
        }

        public BananaMetrics()
        {
        }

        public void addBanana()
        {
            this.Bananas++;
            this.BananaMetric.Set(this.Bananas);
        }

        public int getBananas()
        {
            return this.Bananas;
        }
    }

}
