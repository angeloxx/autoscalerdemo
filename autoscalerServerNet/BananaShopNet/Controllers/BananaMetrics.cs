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
        int getBananasPerMinute();
        void doStats();
    }
    public sealed class BananaMetrics : IBananaMetrics
    {
        private static readonly BananaMetrics _instance = new BananaMetrics();
        private Prometheus.Gauge BananaMetric = Metrics.CreateGauge("win_banana_metrics_counter", "dot.net bananas");
        private Prometheus.Gauge BananaPerMinuteMetric = Metrics.CreateGauge("win_banana_metrics_perminute", "dot.net bananas per minute stats");
        private int Bananas = 0;
        private int BananasPerMinuteCount = 0;
        private int BananasPerMinute = 0;

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
            BananasPerMinuteCount++;
            this.BananaMetric.Set(this.Bananas);
        }

        public int getBananas()
        {
            return this.Bananas;
        }

        public void doStats()
        {
            Console.WriteLine("doStats()");
            this.BananasPerMinute = (int)(((BananasPerMinuteCount * 6) + this.BananasPerMinute) / 2);
            this.BananasPerMinuteCount = 0;
            this.BananaPerMinuteMetric.Set(this.BananasPerMinute);
        }

        public int getBananasPerMinute()
        {
            return this.BananasPerMinute;
        }
    }

}
