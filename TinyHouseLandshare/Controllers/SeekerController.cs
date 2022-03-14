using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    public class LeaseeController : Controller
    {
        [Route("Leasee")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Leasee/{id}")]
        public IActionResult Listing(Guid id)
        {
            return View();
        }
    }
}
