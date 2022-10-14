namespace TinyHouseLandshare.Services
{
    public interface IImageHandlerService
    {
        void SaveImageToStorage(IFormFile image, Guid userId, Guid listingId);
    }
}