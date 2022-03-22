﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserListingRepository _userListingRepository;

        public MessageController(IMessagingService messagingService,
                                 IUserListingRepository userListingRepository)
        {
            _messagingService = messagingService;
            _userListingRepository = userListingRepository;
        }

        public IActionResult Inbox(Guid userId)
        {
            var headMessages = _messagingService.GetMessages(userId);
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
                var recieverId = _userListingRepository.GetUserIdByListing(messageViewModel.ListingId);
                var message = new Message
                {
                    SenderId = messageViewModel.SenderId,
                    LisitingId = messageViewModel.ListingId,
                    ReceiverId = recieverId,
                    TimeStamp = DateTimeOffset.UtcNow,
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