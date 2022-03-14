using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    public class LandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
