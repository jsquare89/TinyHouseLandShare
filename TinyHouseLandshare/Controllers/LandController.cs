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
        private readonly IImageHandlerService _imageHandler;

        public LandController(IListingService listingService,
                              ILandListingRepository landListingRepository,
                              IUserListingRepository userListingRepository,
                              UserManager<UserEntity> userManager,
                              IMapper mapper,
                              IImageHandlerService imageHandler)
        {
            _listingService = listingService;
            _landListingRepository = landListingRepository;
            _userListingRepository = userListingRepository;
            _userManager = userManager;
            _mapper = mapper;
            _imageHandler = imageHandler;
        }

        [Route("Land")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var landListingViewModels = _mapper.Map<IEnumerable<LandListingViewModel>>(_listingService.GetApprovedLandListings());

            foreach (var landListing in landListingViewModels)
            {
                // should get fileName from database. userId_listingId_index.extention. check to see if path exists then populate view model
                var fileName = _imageHandler.GetFileName( landListing.ListerId, landListing.Id, ".jpg");
                landListing.ImageSrc = _imageHandler.GetImageSrc(landListing.ListerId, landListing.Id, fileName);
            }

            return View(landListingViewModels);
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
            var landListingViewModel = _mapper.Map<LandListingViewModel>(landListing);
            landListingViewModel.ListerId = userListing.UserId;
            landListingViewModel.UserListingId = userListing.Id;

            var fileName = _imageHandler.GetImageFileName(landListingViewModel.ListerId, landListingViewModel.Id, "1", ".jpg");
            landListingViewModel.ImageSrc = _imageHandler.GetImageSrc(userListing.UserId, landListing.Id, fileName);

            return View(landListingViewModel);
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
  
            var landListing = _mapper.Map<LandListing>(model);
            landListing = UpdateLandListingWithCreateDefaults( landListing);
            landListing = _listingService.AddLandListing(landListing, LoggedInUserId());
            if(model.MainImage is not null)
            {
                _imageHandler.SaveImageToStorage(model.MainImage, landListing.UserListing.UserId, landListing.Id);
            }
            return RedirectToAction("Dashboard", "Account");
        }

        private static LandListing UpdateLandListingWithCreateDefaults(LandListing landListing)
        {
            var timeStamp = DateTimeOffset.UtcNow;
            landListing.CreatedTime = timeStamp;
            landListing.ModifiedTime = timeStamp;
            landListing.MapLocation = "";
            landListing.Status = "Draft";
            landListing.Approved = false;
            landListing.Submitted = false;
            return landListing;
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
