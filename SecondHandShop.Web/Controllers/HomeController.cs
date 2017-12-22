namespace SecondHandShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecondHandShop.Service;
    using SecondHandShop.Web.Models;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IAdvertisementService advertisement;

        public HomeController(IAdvertisementService advertisement)
        {
            this.advertisement = advertisement;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.advertisement.GetSixAsync();

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}