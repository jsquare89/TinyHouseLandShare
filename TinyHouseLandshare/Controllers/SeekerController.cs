
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Migrations;
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
        private readonly IImageHandlerService _imageHandler;

        public SeekerController(IListingService listingService,
                                IUserListingRepository userListingRepository,
                                UserManager<UserEntity> userManager,
                                IMapper mapper,
                                IImageHandlerService imageHandler)
        {
            _listingService = listingService;
            _userListingRepository = userListingRepository;
            _userManager = userManager;
            _mapper = mapper;
            _imageHandler = imageHandler;
        }
        
        [Route("")]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var seekerListingViewModels = _mapper.Map<IEnumerable<SeekerListingViewModel>>(_listingService.GetApprovedSeekerListings());

            foreach(var seekerListingViewModel in seekerListingViewModels)
            {
                var seekerListingFileName = _imageHandler.GetFileName(seekerListingViewModel.ListerId,
                                                                    seekerListingViewModel.Id,
                                                                    ".jpg");
                seekerListingViewModel.ImageSrc = _imageHandler.GetImageSrc(seekerListingViewModel.ListerId,
                                                                    seekerListingViewModel.Id,
                                                                    seekerListingFileName);
            }
            return View(seekerListingViewModels);
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

            var fileName = _imageHandler.GetImageFileName(seekerListingViewModel.ListerId, seekerListingViewModel.Id, "1", ".jpg");
            seekerListingViewModel.ImageSrc = _imageHandler.GetImageSrc(userListing.UserId, seekerListing.Id, fileName);

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
            if (!ModelState.IsValid)
            {
                // TODO: set response 404 and return error invalid data entered
                return View();
            }
            var seekerListing = _mapper.Map<SeekerListing>(model);
            seekerListing = UpdateSeekerListingWithCreateDefaults(seekerListing);
            _listingService.AddSeekerListing(seekerListing, GetLoggedInUserId());
            if (model.MainImage is not null)
            {
                _imageHandler.SaveImageToStorage(model.MainImage, seekerListing.UserListing.UserId, seekerListing.Id);
            }

            return RedirectToAction("Dashboard", "Account");
        }

        private SeekerListing UpdateSeekerListingWithCreateDefaults(SeekerListing seekerListing)
        {
            var timeStamp = DateTimeOffset.UtcNow;
            seekerListing.Approved = false;
            seekerListing.Status = "Draft";
            seekerListing.Submitted = false;
            seekerListing.Approved = false;
            seekerListing.CreatedTime = timeStamp;
            seekerListing.ModifiedTime = timeStamp;
            return seekerListing;
        }

        private Guid GetLoggedInUserId()
        {
            return new Guid(_userManager.GetUserId(User));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult EditListing(Guid id)
        {
            var seekerListingViewModel = _mapper.Map<SeekerListingViewModel>(_listingService.GetSeekerListing(id));
            if(seekerListingViewModel is null)
            {
                Response.StatusCode = 404;
                return View("SeekerNotFound", id);
            }
            return View(seekerListingViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditListing(SeekerListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seekerListing = _mapper.Map<SeekerListing>(model);
                seekerListing = UpdateSeekerListingWithUpdateDefaults(seekerListing);
                _listingService.UpdateSeekerListing(seekerListing);
                if (model.MainImage is not null)
                {
                    _imageHandler.SaveImageToStorage(model.MainImage, model.ListerId, model.Id);
                }
                return RedirectToAction("Dashboard", "Account");
            }
            // TODO: return error view
            return View();
        }

        private static SeekerListing UpdateSeekerListingWithUpdateDefaults(SeekerListing seekerListing)
        {
            seekerListing.ModifiedTime = DateTimeOffset.UtcNow;
            seekerListing.Status = "Edited";
            seekerListing.Submitted = false;
            seekerListing.Approved = false;
            return seekerListing;
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
            seekerListing.Status = "";
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
