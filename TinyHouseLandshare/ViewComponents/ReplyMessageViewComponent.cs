using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.ViewComponents
{
    public class ReplyMessageViewComponent:ViewComponent
    {
        public ReplyMessageViewComponent()
        {

        }

        public IViewComponentResult Invoke(Guid originMessageId, Guid userListingId, Guid senderId)
        {
            ReplyMessageViewModel viewModel = new ReplyMessageViewModel()
            {
                OriginMessageId = originMessageId,
                UserListingId = userListingId,
                SenderId = senderId,
                Message = ""
            };
            return View(viewModel);
        }
    }
}
