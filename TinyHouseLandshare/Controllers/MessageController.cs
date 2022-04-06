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
            //var headMessages = _messagingService.GetUserMessageHeads(userId);
            //List<HeadMessageViewModel> headMessagesViewModel = new List<HeadMessageViewModel>();
            //foreach(var headMessage in headMessages)
            //{
            //    var headMessageVM = MapHeadMessageToViewModel(headMessage);
            //    headMessagesViewModel.Add(headMessageVM);
            //}
            var viewModel = new MessageInboxViewModel
            {
                headMessages = _messagingService.GetUserMessageHeadsAsViewModels(userId),
                UnreadMessageCount = _messagingService.GetUnreadMessagesCount(userId)
            };
            return View(viewModel);
        }

        //private HeadMessageViewModel MapHeadMessageToViewModel(Message message)
        //{
        //    return new HeadMessageViewModel
        //    {
        //        Id = message.Id,
        //        Title = _listingService.GetListingTitle(message.UserListingId),
        //        SenderId = message.SenderId,
        //        SenderName = _listingService.GetListingSenderName(message.SenderId),
        //        timeStamp = message.TimeStamp,
        //        IsViewed = message.IsViewed,
        //        Value = message.Value
        //    };
        //}

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
