using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;

namespace TinyHouseLandshare.Controllers
{
    public class LandController : Controller
    {
        private readonly ILandListingRepository _landListingRepository;

        public LandController(ILandListingRepository landListingRepository)
        {
            _landListingRepository = landListingRepository;
        }

        [Route("Land")]
        public IActionResult Index()
        {
            var landListings = _landListingRepository.GetAllLandListings();
            return View(landListings);
        }

        [Route("Land/{id}")]
        public IActionResult Listing(Guid id)
        {
            var landListing = _landListingRepository.GetLandListing(id);

            if(landListing is null)
            {
                Response.StatusCode = 404;
                return View("LandNotFound", id);
            }
            return View(landListing);
        }

    }
}
