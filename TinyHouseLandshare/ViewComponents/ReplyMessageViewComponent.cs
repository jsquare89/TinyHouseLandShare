using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.ViewComponents
{
    public class ReplyMessageViewComponent:ViewComponent
    {
        public ReplyMessageViewComponent()
        {

        }

        public IViewComponentResult Invoke(Guid senderId,
                                           Guid receiverId,
                                           Guid userListingId,
                                           Guid originMessageId)
        {
            ReplyMessageViewModel viewModel = new ReplyMessageViewModel()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                UserListingId = userListingId,
                OriginMessageId = originMessageId,                
                Message = ""
            };
            return View(viewModel);
        }
    }
}
