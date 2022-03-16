using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SeekerController : Controller
    {
        private readonly ISeekerListingRepository _seekerListingRepository;
        private readonly IUserSeekerListingRepository _userSeekerListingRepository;
        private readonly UserManager<UserEntity> _userManager;

        public SeekerController(ISeekerListingRepository seekerListingRepository,
                                IUserSeekerListingRepository userSeekerListingRepository,
                                UserManager<UserEntity> userManager)
        {
            _seekerListingRepository = seekerListingRepository;
            _userSeekerListingRepository = userSeekerListingRepository;
            _userManager = userManager;
        }
        
        [Route("")]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var seekerListings = _seekerListingRepository.GetAllSeekerListings();
            return View(seekerListings);
        }


        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult Listing(Guid id)
        {
            var seekerListing = _seekerListingRepository.GetSeekerListing(id);

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
            var seekerListing = new SeekerListing
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
                PreferedLandType = "Residential",
                ParkingRequired = false,
                ChildFriendlyRequired = false,
                PetsRequired = false,
                Smoker = false,
                Privacy = false,
                Approved = false,
                Status = "draft",
                Submitted = false
            };


            seekerListing = _seekerListingRepository.Add(seekerListing);
            var userListing = new UserSeekerListing
            {
                UserId = new Guid(_userManager.GetUserId(User)),
                SeekerListingId = seekerListing.Id
            };
            _userSeekerListingRepository.Add(userListing);

            return RedirectToAction("Dashboard", "Account");
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult EditListing(Guid id)
        {
            var seekerListing = _seekerListingRepository.GetSeekerListing(id);
            var seekerListingViewModel = new SeekerListingViewModel
            {
                Id= id,
                Title = seekerListing.Title,
                Location = seekerListing.Location,
                Details = seekerListing.Details,
            };
            return View(seekerListingViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditListing(SeekerListingViewModel model)
        {
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var seekerListing = new SeekerListing
            {
                Id = model.Id,
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
                PreferedLandType = "Residential",
                ParkingRequired = false,
                ChildFriendlyRequired = false,
                PetsRequired = false,
                Smoker = false,
                Privacy = false,
                Approved = false,
                Status = "updated draft",
                Submitted = false
            };

            seekerListing = _seekerListingRepository.Update(seekerListing);

            return RedirectToAction("Dashboard", "Account");
        }
    }
}
