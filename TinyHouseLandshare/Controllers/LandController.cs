using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    public class LandController : Controller
    {
        public LandController()
        {

        }

        [Route("Land")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Land/{id}")]
        public IActionResult Listing(Guid id)
        {
            return View();
        }

    }
}
