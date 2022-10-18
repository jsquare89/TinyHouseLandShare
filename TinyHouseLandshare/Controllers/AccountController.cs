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
        private readonly IImageHandlerService _imageHandler;

        public AccountController(UserManager<UserEntity> userManager, 
                                 SignInManager<UserEntity> signInManager,
                                 IListingService listingService,
                                 IMapper mapper,
                                 IImageHandlerService imageHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _listingService = listingService;
            _mapper = mapper;
            _imageHandler = imageHandler;
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
                SeekerListing = _mapper.Map<SeekerListingViewModel>(_listingService.GetUserSeekerListing(userId)),
                LandListings = _mapper.Map<IEnumerable<LandListingViewModel>>(_listingService.GetUserLandListings(userId).OrderByDescending(l => l.ModifiedTime))
            };

            if(userListings.SeekerListing is not null)
            {
                var seekerListingFileName = _imageHandler.GetFileName(userId, userListings.SeekerListing.Id, ".jpg");
                userListings.SeekerListing.ImageSrc = _imageHandler.GetImageSrc(userId,
                       userListings.SeekerListing.Id,
                       seekerListingFileName);
            }

            foreach(var landListing in userListings.LandListings)
            {
                // should get fileName from database. userId_listingId_index.extention. check to see if path exists then populate view model
                var landListingFileName = _imageHandler.GetFileName( userId, landListing.Id, ".jpg");
                landListing.ImageSrc = _imageHandler.GetImageSrc(userId, landListing.Id, landListingFileName);      
            }
            return View(userListings);
        }

    }
}
