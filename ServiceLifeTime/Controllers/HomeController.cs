using Microsoft.AspNetCore.Mvc;
using ServiceLifeTime.Models;
using ServiceLifeTime.Services;
using System.Diagnostics;
using System.Text;

namespace ServiceLifeTime.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ISingleToneService singleToneService1;
        private readonly ISingleToneService singleToneService2;
        private readonly IScopedService scopedService1;
        private readonly IScopedService scopedService2;
        private readonly ITransientService transientService1;
        private readonly ITransientService transientService2;

        public HomeController(ISingleToneService singleToneService1, ISingleToneService singleToneService2, IScopedService scopedService1, IScopedService scopedService2, ITransientService transientService1, ITransientService transientService2)
        {
            this.singleToneService1 = singleToneService1;
            this.singleToneService2 = singleToneService2;
            this.scopedService1 = scopedService1;
            this.scopedService2 = scopedService2;
            this.transientService1 = transientService1;
            this.transientService2 = transientService2;
        }

        public string Index()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SingleTone1{singleToneService1.GetGuid()}");
            sb.AppendLine($"SingleTone2{singleToneService2.GetGuid()}");
            sb.AppendLine($"Scoped1{scopedService1.GetGuid()}");
            sb.AppendLine($"Scoped2{scopedService2.GetGuid()}");
            sb.AppendLine($"Transient1{transientService1.GetGuid()}");
            sb.AppendLine($"Transient2{transientService2.GetGuid()}");
         
            return sb.ToString();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
