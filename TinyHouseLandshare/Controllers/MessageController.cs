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

        public IActionResult Send(MessageViewModel messageVM)
        {
            if (ModelState.IsValid)
            {
                _messagingService.SendInitialdMessage(messageVM.SenderId, messageVM.ReceiverId, messageVM.UserListingId, messageVM.Message);
                return RedirectToAction("Dashboard", "Account");
            }
            return BadRequest();
        }

        public IActionResult SendReply(ReplyMessageViewModel messageVM)
        {
            if (ModelState.IsValid)
            {
                _messagingService.SendReplyMessage(messageVM.OriginMessageId, messageVM.SenderId, messageVM.UserListingId, messageVM.Message);
                return RedirectToAction("Message", "Message", new {id=messageVM.OriginMessageId});
            }
            return BadRequest();
        }

        public IActionResult Message(Guid id)
        {
            var sender = _messagingService.GetOriginMessageSender(id);
            var messages = _messagingService.GetMessagesByHeadId(id);
            var viewMessageViewModel = new ViewMessageViewModel
            {
                OriginMessageId = id,
                OrginMessageSenderId = sender.id,
                OriginMessageSenderName = sender.name,
                UserListingId = _messagingService.GetMessageUserListingId(id),
                Messages = ConvertMessagesToMessageViewModel(messages)
            };
            return View(viewMessageViewModel);
        }

        private IEnumerable<MessageViewModel> ConvertMessagesToMessageViewModel(IEnumerable<Message> messages)
        {
            var messageViewModelList = new List<MessageViewModel>();
            foreach(var message in messages)
            {
                var messageViewModel = new MessageViewModel
                {
                    SenderId = message.SenderId,
                    Message = message.Value,
                    TimeStamp = message.TimeStamp
                };

                messageViewModelList.Add(messageViewModel);
            }
            return messageViewModelList;
        }


    }
}
