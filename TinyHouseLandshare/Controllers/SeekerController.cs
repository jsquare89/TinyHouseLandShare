using Microsoft.AspNetCore.Mvc;

namespace TinyHouseLandshare.Controllers
{
    [Route("[controller]")]
    public class SeekerController : Controller
    {
        
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("{id}")]
        public IActionResult Listing(Guid id)
        {
            return View();
        }
    }
}
