using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.UserSecrets;
using TinyHouseLandshare.Models;

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

    /// <summary>
    /// Returns image files name in the format: userId_listingId_index.extention
    /// e.g. userId_listingId_1.jpg
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="listingId"></param>
    /// <param name="index">unique identifier for listing image</param>
    /// <param name="extention">the image extention with . (period)</param>
    /// <returns></returns>
    public string GetImageFileName(Guid userId, Guid listingId, string index, string extention)
    {
        return $"{userId}_{listingId}_{index}{extention}";
    }

    /// <summary>
    /// Returns the HTML src file path for the image in correct format if file exists in storage
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="listingId"></param>
    /// <param name="fileName"></param>
    /// <returns>return src string if exists, else returns null</returns>
    public string? GetImageSrc(Guid userId, Guid listingId, string fileName)
    {
        var filePath = GetFilePath(userId, listingId, fileName);    
        if (System.IO.File.Exists(filePath))
        {
            return $"/{_listingImagePath}/{userId.ToString()}/{listingId}/{fileName}";
        }
        return null;
    }

    private string? GetFilePath(IFormFile image, Guid userId, Guid listingId)
    {
        var folderPath = CreateFolderPath(listingId, userId);
        if(folderPath is not null)
        {
            var fileName = GetFileName(listingId,
                                          userId,
                                          Path.GetExtension(image.FileName));
            return Path.Combine(folderPath, fileName);
        }
        return null;
    }

    private string GetFilePath(Guid userId, Guid listingId, string fileName)
    {
        return Path.Combine(GetFolderPath(userId, listingId), fileName);
    }

    public string GetFileName(Guid userId, Guid listingId, string extention)
    {
        //TODO: check folder and get next fileIndex for fileName if images exist else start at 1
        var fileIndex = 1;
        return userId + "_" + listingId + "_" + fileIndex + extention;
    }

    public string GetFolderPath(Guid userId, Guid listingId)
    {
        return Path.Combine(_webHostEnvironment.WebRootPath,
                            _listingImagePath,
                            userId.ToString(),
                            listingId.ToString());
    }

    private string? CreateFolderPath(Guid listingId, Guid userId)
    {
        var folderPath = GetFolderPath(listingId, userId);

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
