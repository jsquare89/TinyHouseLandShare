using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    public class LeaseeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
