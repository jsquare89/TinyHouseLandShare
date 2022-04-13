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

        public IViewComponentResult Invoke(Guid listingId, Guid senderId)
        {
            MessageViewModel viewModel = new MessageViewModel()
            {
                SeekerOrLandListingId = listingId,
                SenderId = senderId,
                Message = ""
            };
            return View(viewModel);
        }
    }
}
