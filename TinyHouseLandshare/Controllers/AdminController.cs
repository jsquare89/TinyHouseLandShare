using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;
        private readonly IImageHandlerService _imageHandler;

        public AdminController(IListingService listingService, 
                               IMapper mapper,
                               IImageHandlerService imageHandler)
        {
            _listingService = listingService;
            _mapper = mapper;
            _imageHandler = imageHandler;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ApproveListing()
        {
            var approveListingsViewModel = new ApproveListingViewModel
            {
                seekerListings = _mapper.Map<IEnumerable<SeekerListingViewModel>>(_listingService.GetAllUnapprovedSubmittedSeekerListings()),
                landListings = _mapper.Map<IEnumerable<LandListingViewModel>>(_listingService.GetAllUnApprovedSubmittedLandListings())
            };

            foreach(var seekerListing in approveListingsViewModel.seekerListings)
            {
                var seekerListingFileName = _imageHandler.GetFileName(seekerListing.ListerId, seekerListing.Id, ".jpg");
                seekerListing.ImageSrc = _imageHandler.GetImageSrc(seekerListing.ListerId, seekerListing.Id, seekerListingFileName);
            }

            foreach (var landListing in approveListingsViewModel.landListings)
            {
                // should get fileName from database. userId_listingId_index.extention. check to see if path exists then populate view model
                var landListingFileName = _imageHandler.GetFileName(landListing.ListerId, landListing.Id, ".jpg");
                landListing.ImageSrc = _imageHandler.GetImageSrc(landListing.ListerId, landListing.Id, landListingFileName);
            }

            return View(approveListingsViewModel);
        }

        [HttpPost]
        public IActionResult ApproveSeekerListing(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _listingService.GetSeekerListing(id);
            listing.Approved = true;
            listing.Status = "Posted";
            _listingService.UpdateSeekerListing(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }

        [HttpPost]
        public IActionResult RejectSeekerListing(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _listingService.GetSeekerListing(id);
            listing.Approved = false;
            listing.Submitted = false;
            listing.Status = "Rejected";
            _listingService.UpdateSeekerListing(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }

        [HttpPost]
        public IActionResult ApproveLandListing(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _listingService.GetLandListing(id);
            listing.Approved = true;
            listing.Status = "Posted";
            _listingService.UpdateLandListing(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }

        [HttpPost]
        public IActionResult RejectLandListing(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _listingService.GetLandListing(id);
            listing.Approved = false;
            listing.Submitted = false;
            listing.Status = "Rejected";
            _listingService.UpdateLandListing(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }
    }
}
