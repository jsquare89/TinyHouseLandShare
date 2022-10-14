using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<UserEntity> userManager, 
                                 SignInManager<UserEntity> signInManager,
                                 IListingService listingService,
                                 IMapper mapper,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _listingService = listingService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedAt = DateTimeOffset.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Dashboard", "Account");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Account");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var userId = new Guid(_userManager.GetUserId(User));
            var userListings = new UserListingsViewModel
            {
                SeekerListing = _listingService.GetUserSeekerListing(userId),
                LandListings = _mapper.Map<IEnumerable<LandListingViewModel>>(_listingService.GetUserLandListings(userId))
            };


            foreach(var landListing in userListings.LandListings)
            {
                var folderPath = GenerateFolderPath(landListing.Id, userId);
                var fileName = Path.Combine(folderPath, GetNewFileName(".jpg" , landListing.Id, userId));
                if (System.IO.File.Exists(fileName))
                {
                    landListing.ImageUrl = "/listing_images/" + userId + "/" + landListing.Id + "/" + userId + "_" + landListing.Id + "_1.jpg";
                }
                else
                {
                    landListing.ImageUrl = null;
                }
            }

            return View(userListings);
        }

        private string GetNewFileName(string extention, Guid listingId, Guid userId)
        {
            //TODO: check folder and get next fileIndex for fileName if images exist else start at 1
            var fileIndex = 1;
            return userId + "_" + listingId + "_" + fileIndex + extention;
        }

        private string GenerateFolderPath(Guid listingId, Guid userId)
        {
            string baseFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "listing_images");
            var uniqueFolderName = Path.Combine(baseFilePath, userId.ToString(), listingId.ToString());
            return uniqueFolderName;
        }
    }
}
