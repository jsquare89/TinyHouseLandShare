using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Route("[controller]")]
    public class SeekerController : Controller
    {
        private readonly ISeekerListingRepository _seekerPostRepository;

        public SeekerController(ISeekerListingRepository seekerPostRepository)
        {
            _seekerPostRepository = seekerPostRepository;
        }
        
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            var seekerListings = _seekerPostRepository.GetAllSeekerListings();
            return View(seekerListings);
        }


        [Route("{id}")]
        public IActionResult Listing(Guid id)
        {
            var seekerListing = _seekerPostRepository.GetSeekerListing(id);

            if(seekerListing is null)
            {
                Response.StatusCode = 404;
                return View("SeekerNotFound", id);

            }
            return View(seekerListing);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult CreateListing()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateListing(SeekerListingViewModel model)
        {
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var seekerPost = new SeekerListing
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = DateTimeOffset.UtcNow,
                PictureUri = "",
                HouseSize = "",
                OccupantCount = 0,
                WifiConnectionRequired = false,
                WaterConnectionRequired = false,
                ElectricalConnectionRequired = false,
                ParkingRequired = false,
                ChildFriendlyRequired = false,
                PetsRequired = false,
                Smoker = false,
                Privacy = false
            };

            _seekerPostRepository.Add(seekerPost);

            return RedirectToAction("Index");
        }
    }
}
