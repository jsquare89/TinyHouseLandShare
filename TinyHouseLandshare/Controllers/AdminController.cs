using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IListingService _listingService;
        private readonly ISeekerListingRepository _seekerListingRepository;
        private readonly ILandListingRepository _landListingRepository;

        public AdminController(IListingService listingService,
                               ISeekerListingRepository seekerListingRepository,
                               ILandListingRepository landListingRepository)
        {
            _listingService = listingService;
            _seekerListingRepository = seekerListingRepository;
            _landListingRepository = landListingRepository;
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
            var seekerListingsViewModel = new ApproveListingViewModel
            {
                seekerListings = _seekerListingRepository.GetAllUnapprovedSubmittedSeekerListings(),
                landListings = _landListingRepository.GetAllUnApprovedSubmittedLandListings()
            };

            return View(seekerListingsViewModel);
        }

        [HttpPost]
        public IActionResult ApproveSeekerListing(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _seekerListingRepository.GetSeekerListing(id);
            listing.Approved = true;
            listing.Status = "Posted";
            _seekerListingRepository.Update(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }

        [HttpPost]
        public IActionResult ApproveLandListing(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var listing = _landListingRepository.GetLandListing(id);
            listing.Approved = true;
            listing.Status = "Posted";
            _landListingRepository.Update(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }
    }
}
