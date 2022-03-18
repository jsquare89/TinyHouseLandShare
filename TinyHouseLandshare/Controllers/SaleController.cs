using Microsoft.AspNetCore.Mvc;


namespace TinyHouseLandshare.Controllers
{
    public class SaleController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
