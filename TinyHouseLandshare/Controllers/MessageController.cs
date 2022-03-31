using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Data;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessagingService _messagingService;
        private readonly IListingService _listingService;

        public MessageController(IMessagingService messagingService,
                                 IListingService listingService)
        {
            _messagingService = messagingService;
            _listingService = listingService;
        }

        public IActionResult Inbox(Guid userId)
        {
            var headMessages = _messagingService.GetUserMessageHeads(userId);
            var unreadMessageCount = _messagingService.GetUnreadMessagesCount(userId);
            var viewModel = new MessageInboxViewModel
            {
                headMessages = headMessages,
                UnreadMessageCount = unreadMessageCount
            };
            return View(viewModel);
        }

        public IActionResult Send(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var listingId = _listingService.GetListingIdBySeekerOrLandListingId(messageViewModel.SeekerOrLandListingId);
                _messagingService.SendMessage(messageViewModel.SenderId, listingId, messageViewModel.Message);
                return RedirectToAction("Dashboard", "Account");
            }
            return BadRequest();
        }
    }
}
