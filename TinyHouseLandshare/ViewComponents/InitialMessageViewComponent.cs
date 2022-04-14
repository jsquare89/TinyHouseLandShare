using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.ViewComponents
{
    public class InitialMessageViewComponent: ViewComponent
    {
        private readonly IMessagingService _messagingService;

        public InitialMessageViewComponent(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public IViewComponentResult Invoke(Guid userListingId, Guid senderId, Guid receiverId)
        {
            MessageViewModel viewModel = new MessageViewModel()
            {
                UserListingId = userListingId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = ""
            };
            return View(viewModel);
        }
    }
}
