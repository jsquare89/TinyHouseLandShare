using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessagingService _messagingService;
        private readonly IUserListingRepository _userListingRepository;

        public MessageController(IMessagingService messagingService,
                                 IUserListingRepository userListingRepository)
        {
            _messagingService = messagingService;
            _userListingRepository = userListingRepository;
        }


        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    SenderId = messageViewModel.SenderId,
                    LisitingId = messageViewModel.ListingId,
                    ReceiverId = _userListingRepository.GetUserIdByListing(messageViewModel.ListingId),
                    Value = messageViewModel.Message,
                    IsViewed = false,
                    ParentMessageId = Guid.Empty
                };
                _messagingService.SendMessage(message);
                return RedirectToAction("Dashboard", "Account");
            }
            return BadRequest();
        }
    }
}
