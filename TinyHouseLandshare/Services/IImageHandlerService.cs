namespace TinyHouseLandshare.Services
{
    public interface IImageHandlerService
    {
        string GetFileName(Guid userId, Guid listingId, string extention);
        string GetFolderPath(Guid userId, Guid listingId);
        string GetImageFileName(Guid userId, Guid listingId, string index, string extention);
        string? GetImageSrc(Guid userId, Guid listingId, string fileName);
        void SaveImageToStorage(IFormFile image, Guid userId, Guid listingId);
        void UpdateMainImage(IFormFile mainImage, Guid userId, Guid id);
    }
}