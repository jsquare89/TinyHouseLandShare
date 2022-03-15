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
            var seekerPost = _seekerPostRepository.GetSeekerPost(id);
            return View(seekerPost);
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
