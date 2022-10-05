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
    [Authorize]
    public class LandController : Controller
    {
        private readonly IListingService _listingService;
        private readonly ILandListingRepository _landListingRepository;
        private readonly IUserListingRepository _userListingRepository;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public LandController(IListingService listingService,
                              ILandListingRepository landListingRepository,
                              IUserListingRepository userListingRepository,
                              UserManager<UserEntity> userManager,
                              IMapper mapper)
        {
            _listingService = listingService;
            _landListingRepository = landListingRepository;
            _userListingRepository = userListingRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("Land")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var landListings = _listingService.GetApprovedLandListings();
            return View(landListings);
        }

        [Route("Land/{id}")]
        [AllowAnonymous]
        public IActionResult Listing(Guid id)
        {
            var landListing = _listingService.GetLandListing(id);
            if(landListing is null)
            {
                Response.StatusCode = 404;
                return View("LandNotFound", id);
            }

            var userListing = _userListingRepository.GetUserListingBySeekerOrLandListingId(id);
            if(userListing is null)
            {
                Response.StatusCode = 404;
                return View("LandNotFound", id);
            }
            var landListingVM = _mapper.Map<LandListingViewModel>(landListing);
            landListingVM.ListerId = userListing.UserId ;
            landListingVM.UserListingId = userListing.Id;

            return View(landListingVM);
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters
            var timeStamp = DateTimeOffset.UtcNow;
            var landListing = new LandListing
            {
                Title = model.Title,
                Location = model.Location,
                Details = model.Details,
                CreatedTime = timeStamp,
                ModifiedTime = timeStamp,
                Price = model.Price,
                MapLocation = "",
                AvailableDate = model.AvailableDate,
                LandType = model.LandType,
                SiteFoundation = model.SiteFoundation,
                DrivewayFoundation = model.DrivewayFoundation,
                WifiConnection = model.WifiConnection,
                WaterConnection = model.WaterConnection,
                ElectricalConnection = model.ElectricalConnection,
                Parking = model.Parking,
                ChildFriendly = model.ChildFriendly,
                PetFriendly = model.PetFriendly,
                NoSmoking = model.NoSmoking,
                Private = model.Private,
                Status = "Draft",
                Approved = false,
                Submitted = false,
                Country = model.Country,
                State = model.State
            };

            _listingService.AddLandListing(landListing, LoggedInUserId());
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
            var landListing = _listingService.GetLandListing(id);
            var landListingViewModel = new LandListingViewModel
            {
                Id = id,
                Title = landListing.Title,
                Location = landListing.Location,
                Details = landListing.Details,
                State = landListing.State,
                Country = landListing.Country,
                Price = landListing.Price,
                AvailableDate = landListing.AvailableDate,
                ModifiedTime = landListing.ModifiedTime,
                LandType = landListing.LandType,
                SiteFoundation = landListing.SiteFoundation,
                DrivewayFoundation = landListing.DrivewayFoundation,
                Private = landListing.Private,
                WaterConnection = landListing.WaterConnection,
                ElectricalConnection = landListing.ElectricalConnection,
                WifiConnection = landListing.WifiConnection,
                PetFriendly = landListing.PetFriendly,
                ChildFriendly = landListing.ChildFriendly ,
                NoSmoking = landListing.NoSmoking,
                Parking = landListing.Parking
            };
            return View(landListingViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditListing(LandListingViewModel model)
        {
            if(ModelState.IsValid)
            {
                var landListing = _listingService.GetLandListing(model.Id);
                landListing.Title = model.Title;
                landListing.Location = model.Location;
                landListing.State = model.State;
                landListing.Country = model.Country;
                landListing.Details = model.Details;
                landListing.MapLocation = "";
                landListing.AvailableDate = model.AvailableDate;
                landListing.Price = model.Price;
                landListing.LandType = model.LandType;
                landListing.SiteFoundation = model.SiteFoundation;
                landListing.DrivewayFoundation = model.DrivewayFoundation;
                landListing.WaterConnection = model.WaterConnection;
                landListing.ElectricalConnection = model.ElectricalConnection;
                landListing.WifiConnection = model.WifiConnection;
                landListing.PetFriendly = model.PetFriendly;
                landListing.ChildFriendly = model.ChildFriendly;
                landListing.NoSmoking = model.NoSmoking;
                landListing.Private = model.Private;
                landListing.Parking = model.Parking;

                landListing.ModifiedTime = DateTimeOffset.UtcNow;
                landListing.Approved = false;
                landListing.Submitted = false;
                landListing.Status = "Edited Draft";

                _listingService.UpdateLandListing(landListing);
                return RedirectToAction("Dashboard", "Account");
            }
            // TODO: fill out the rest of the model, dummy default values used below. Form needs to accept all parameters

            return View();           
        }

        [Route("[action]")]
        public IActionResult DeleteListing(Guid id)
        {
            _listingService.DeleteLandListing(id);
            return RedirectToAction("Dashboard", "Account");
        }

        [Route("[action]")]
        public IActionResult SubmitApproval(Guid id)
        {
            var landListing = _listingService.GetLandListing(id);
            landListing.Submitted = true;
            landListing.Status = "Submitted";
            _listingService.UpdateLandListing(landListing);
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
        public IActionResult Search(LandSearchFilter landSearchFilter)
        {
            var filteredListings = _landListingRepository.Search(landSearchFilter);
            return View("Index", filteredListings);
        }
    }
}
