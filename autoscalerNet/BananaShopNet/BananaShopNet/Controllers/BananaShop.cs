using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace BananaShopNet.Controllers
{
    [Route("[controller]")]
    public class BananaShopController : Controller
    {
        private readonly IBananaMetrics bananaMetrics;

        public BananaShopController(IBananaMetrics inMetrics)
        {
            this.bananaMetrics = inMetrics;
        }

        [HttpGet("/askForBanana")]
        public ActionResult askForBanana()
        {

            this.bananaMetrics.addBanana();
            return StatusCode(200, Json(new { status = "OK" }));
        }

        [HttpGet("/getTotalBananas")]
        public ActionResult getTotalBananas()
        {
            return StatusCode(200, Json(new { bananas = this.bananaMetrics.getBananas() } ));
        }
    }

}
