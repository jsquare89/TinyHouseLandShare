using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    public class SeekerController : Controller
    {
        [Route("Seeker")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Seeker/{id}")]
        public IActionResult Listing(Guid id)
        {
            return View();
        }
    }
}
