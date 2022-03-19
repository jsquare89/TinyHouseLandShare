using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.ViewComponents
{
    public class MessageViewComponent: ViewComponent
    {
        private readonly IMessagingService _messagingService;

        public MessageViewComponent(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public IViewComponentResult Invoke(Guid listingId, Guid senderId)
        {
            MessageViewModel viewModel = new MessageViewModel()
            {
                ListingId = listingId,
                SenderId = senderId,
                Message = ""
            };
            return View(viewModel);
        }
    }
}
