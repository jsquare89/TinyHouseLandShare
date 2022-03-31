using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SeekerController : Controller
    {
        private readonly IListingService _listingService;
        private readonly UserManager<UserEntity> _userManager;

        public SeekerController(IListingService listingService,
                                UserManager<UserEntity> userManager)
        {
            _listingService = listingService;
            _userManager = userManager;
        }
        
        [Route("")]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var seekerListings = _listingService.GetApprovedSeekerListings();
            return View(seekerListings);
        }


        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult Listing(Guid id)
        {
            var seekerListing = _listingService.GetSeekerListing(id);

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

            var timeStamp = DateTimeOffset.UtcNow;
            var seekerListing = new SeekerListing
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = timeStamp,
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
                Privacy = "Community",
                Approved = false,
                Status = "draft",
                Submitted = false,
                Country = "CA",
                State = "BC",
                ModifiedTime = timeStamp
            };


            _listingService.AddSeekerListing(seekerListing, LoggedInUserId());

            return RedirectToAction("Dashboard", "Account");
        }

        private Guid LoggedInUserId()
        {
            return new Guid(_userManager.GetUserId(User));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult EditListing(Guid id)
        {
            var seekerListing = _listingService.GetSeekerListing(id);
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
            if (ModelState.IsValid)
            {
                var seekerListing = _listingService.GetSeekerListing(model.Id);
                seekerListing.Title = model.Title;
                seekerListing.Location = model.Location;
                seekerListing.Details = model.Details;
                seekerListing.ModifiedTime = DateTimeOffset.UtcNow;
                seekerListing.Status = "Edited - Unapproved";
                seekerListing.Submitted = false;
                seekerListing.Approved = false;

                _listingService.UpdateSeekerListing(seekerListing);
                return RedirectToAction("Dashboard", "Account");
            }
            return View();
        }

        [Route("[action]")]
        public IActionResult DeleteListing(Guid id)
        {
            _listingService.DeleteSeekerListing(id);
            return RedirectToAction("Dashboard", "Account");
        }

        [Route("[action]")]
        public IActionResult SubmitApproval(Guid id)
        {
            var seekerListing = _listingService.GetSeekerListing(id);
            seekerListing.Submitted = true;
            seekerListing.Status = "Submitted for approval";
            _listingService.UpdateSeekerListing(seekerListing);
            return RedirectToAction("Dashboard", "Account");
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Search(SeekerSearchFilter seekerSearchFilter)
        {
            var filteredListings = _listingService.SearchSeekerListings(seekerSearchFilter);
            return View("Index", filteredListings);
        }
    }
}
