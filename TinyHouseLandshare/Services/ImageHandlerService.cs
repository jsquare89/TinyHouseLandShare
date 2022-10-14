using Microsoft.AspNetCore.Hosting;

namespace TinyHouseLandshare.Services;

public class ImageHandlerService : IImageHandlerService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _listingImagePath = "listing_images";

    public ImageHandlerService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    //CONSIDER MAKING BOOL TO INDICATE SAVED OR NOT
    public void SaveImageToStorage(IFormFile image, Guid userId, Guid listingId)
    {
        string filePath = GetFilePath(image, userId, listingId);

        if(filePath is not null)
        {
            try
            {
                image.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    private string? GetFilePath(IFormFile image, Guid userId, Guid listingId)
    {
        var folderPath = GetFolderPath(listingId, userId);
        if(folderPath is not null)
        {
            var fileName = GetFileName(listingId,
                                          userId,
                                          Path.GetExtension(image.FileName));
            return Path.Combine(folderPath, fileName);
        }
        return null;
    }

    private string GetFileName(Guid listingId, Guid userId, string extention)
    {
        //TODO: check folder and get next fileIndex for fileName if images exist else start at 1
        var fileIndex = 1;
        return userId + "_" + listingId + "_" + fileIndex + extention;
    }

    private string? GetFolderPath(Guid listingId, Guid userId)
    {
        var folderPath =  Path.Combine(_webHostEnvironment.WebRootPath,
                            _listingImagePath,
                            userId.ToString(),
                            listingId.ToString());

        if (Directory.Exists(folderPath))
        {
            return folderPath;
        }
        else
        {
            try
            {
                Directory.CreateDirectory(folderPath);
                return folderPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }

}
