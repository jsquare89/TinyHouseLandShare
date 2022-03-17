using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize]
    public class LandController : Controller
    {
        private readonly ILandListingRepository _landListingRepository;
        private readonly IUserLandListingRepository _userLandListingRepository;
        private readonly UserManager<UserEntity> _userManager;

        public LandController(ILandListingRepository landListingRepository,
                              IUserLandListingRepository userLandListingRepository,
                              UserManager<UserEntity> userManager)
        {
            _landListingRepository = landListingRepository;
            _userLandListingRepository = userLandListingRepository;
            _userManager = userManager;
        }

        [Route("Land")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var landListings = _landListingRepository.GetAllLandListings();
            return View(landListings);
        }

        [Route("Land/{id}")]
        [AllowAnonymous]
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

        [HttpGet]
        [Route("[action]")]
        public IActionResult CreateListing()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateListing(LandListingViewModel model)
        {
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var landListing = new LandListing
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = DateTimeOffset.UtcNow,
                PictureUri = "",
                MapLocation = "coords go here",
                Price = "600 per month",
                AvailableDate = new DateTimeOffset(2022, 04, 1, 0, 0, 0, TimeSpan.Zero),
                LotSize = "20x40ft 800sqft",
                LandType = "Commercial",
                FoundationSize = "12x30ft",
                SiteFoundation = "concrete",
                DrivewayFoundation = "gravel",
                WifiConnection = "Yes",
                WaterConnection = "Yes",
                ElectricalConnection = "50Amp",
                Parking = "On site",
                ChildFriendly = "True",
                Pets = "True",
                SmokingPermitted = "True",
                Privacy = "True",
                Approved = false,
                Status = "draft",
                Submitted = false   
            };

            landListing = _landListingRepository.Add(landListing);

            var userListing = new UserLandListing
            {
                UserId = new Guid(_userManager.GetUserId(User)),
                LandListingId = landListing.Id
            };
            _userLandListingRepository.Add(userListing);

            return RedirectToAction("Dashboard", "Account");
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult EditListing(Guid id)
        {
            var landListing = _landListingRepository.GetLandListing(id);
            var landListingViewModel = new LandListingViewModel
            {
                Id = id,
                Title = landListing.Title,
                Location = landListing.Location,
                Details = landListing.Details,
            };
            return View(landListingViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditListing(LandListingViewModel model)
        {
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var landListing = new LandListing
            {
                Id = model.Id,
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = DateTimeOffset.UtcNow,
                PictureUri = "",
                MapLocation = "coords go here",
                Price = "600 per month",
                AvailableDate = new DateTimeOffset(2022, 04, 1, 0, 0, 0, TimeSpan.Zero),
                LotSize = "20x40ft 800sqft",
                LandType = "Commercial",
                FoundationSize = "12x30ft",
                SiteFoundation = "concrete",
                DrivewayFoundation = "gravel",
                WifiConnection = "Yes",
                WaterConnection = "Yes",
                ElectricalConnection = "50Amp",
                Parking = "On site",
                ChildFriendly = "True",
                Pets = "True",
                SmokingPermitted = "True",
                Privacy = "True",
                Approved = false,
                Status = "draft",
                Submitted = false
            };

            landListing = _landListingRepository.Update(landListing);

            return RedirectToAction("Dashboard", "Account");
        }

        [Route("[action]")]
        public IActionResult DeleteListing(Guid id)
        {
            var userListingToDelete = new UserLandListing
            {
                UserId = new Guid(_userManager.GetUserId(User)),
                LandListingId = id
            };
            _userLandListingRepository.Delete(userListingToDelete);
            _landListingRepository.Delete(id);

            return RedirectToAction("Dashboard", "Account");
        }

        [Route("[action]")]
        public IActionResult SubmitApproval(Guid id)
        {
            var landListing = _landListingRepository.GetLandListing(id);
            landListing.Submitted = true;
            landListing.Status = "Submitted for approval";
            _landListingRepository.Update(landListing);
            return RedirectToAction("Dashboard", "Account");
        }





    }
}
