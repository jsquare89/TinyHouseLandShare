using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Route("[controller]")]
    public class SeekerController : Controller
    {
        private readonly ISeekerPostRepository _seekerPostRepository;

        public SeekerController(ISeekerPostRepository seekerPostRepository)
        {
            _seekerPostRepository = seekerPostRepository;
        }
        
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            var model = _seekerPostRepository.GetAllSeekerPost();
            return View(model);
        }


        [Route("{id}")]
        public IActionResult Listing(Guid id)
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult CreateListing()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateListing(SeekerPostViewModel model)
        {
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var seekerPost = new SeekerPost
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = DateTimeOffset.UtcNow,
                PictureUri = "",
                HouseSize = "26'x8' 200sqft",
                OccupantCount = 1,
                WifiConnectionRequired = true,
                WaterConnectionRequired = true,
                ElectricalConnectionRequired = true,
                ParkingRequired = true,
                ChildFriendlyRequired = false,
                PetsRequired = true,
                Smoker = false,
                Privacy = true
            };

            _seekerPostRepository.Add(seekerPost);

            return RedirectToAction("Index");
        }
    }
}
