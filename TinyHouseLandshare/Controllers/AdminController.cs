using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ISeekerListingRepository _seekerListingRepository;

        public AdminController(ISeekerListingRepository seekerListingRepository)
        {
            _seekerListingRepository = seekerListingRepository;
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
            var seekerListingsForApproval = _seekerListingRepository.GetAllUnapprovedSubmittedSeekerListings();
            var seekerListingsViewModel = new ApproveListingViewModel
            {
                seekerListings = seekerListingsForApproval,
                landListings = null
            };

            return View(seekerListingsViewModel);
        }

        [HttpPost]
        public IActionResult ApproveListing(Guid id)
        {
            var listing = _seekerListingRepository.GetSeekerListing(id);
            listing.Approved = true;
            listing.Status = "Posted";
            _seekerListingRepository.Update(listing);

            return RedirectToAction("ApproveListing", "Admin");
        }
    }
}
