namespace WhalleyClimate.Web.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task<bool> DeleteImageAsync(string relativePath);
}