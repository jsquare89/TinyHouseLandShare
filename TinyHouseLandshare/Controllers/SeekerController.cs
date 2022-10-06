using AutoMapper;
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
        private readonly IUserListingRepository _userListingRepository;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public SeekerController(IListingService listingService,
                                IUserListingRepository userListingRepository,
                                UserManager<UserEntity> userManager,
                                IMapper mapper)
        {
            _listingService = listingService;
            _userListingRepository = userListingRepository;
            _userManager = userManager;
            _mapper = mapper;
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
            var userListing = _userListingRepository.GetUserListingBySeekerOrLandListingId(id);
            if (userListing is null)
            {
                Response.StatusCode = 404;
                return View("LandNotFound", id);
            }
            var seekerListingViewModel = _mapper.Map<SeekerListingViewModel>(seekerListing);
            seekerListingViewModel.ListerId = userListing.UserId;
            seekerListingViewModel.UserListingId = userListing.Id;

            return View(seekerListingViewModel);
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
                InternetConnectionRequired = false,
                WaterConnectionRequired = false,
                ElectricalConnectionRequired = false,
                PreferedLandType = "Residential",
                ParkingRequired = false,
                ChildFriendlyRequired = false,
                PetsRequired = false,
                Approved = false,
                Status = "draft",
                Submitted = false,
                Country = "CA",
                State = "BC",
                ModifiedTime = timeStamp
            };


            _listingService.AddSeekerListing(seekerListing, GetLoggedInUserId());

            return RedirectToAction("Dashboard", "Account");
        }

        private Guid GetLoggedInUserId()
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
