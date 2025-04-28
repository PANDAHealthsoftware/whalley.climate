// File: Services/ImageService.cs
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WhalleyClimate.Web.Services;
using WorleyClimate.Web.Services;

namespace WorleyClimate.Web.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadFolder;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Unsupported file type.");

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path for use in the database
            return $"/uploads/{fileName}";
        }

        public Task<bool> DeleteImageAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.FromResult(false);

            var fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
