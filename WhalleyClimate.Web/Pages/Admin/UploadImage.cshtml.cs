using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhalleyClimate.Web.Data;
using WhalleyClimate.Web.Models;
using WhalleyClimate.Web.Services;

namespace WhalleyClimate.Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UploadImageModel : PageModel
    {
        private readonly IImageService _imageService;
        private readonly WhalleyClimateDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UploadImageModel(IImageService imageService, WhalleyClimateDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _imageService = imageService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public string? Description { get; set; }

        public bool UploadSuccess { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select an image to upload.");
                return Page();
            }

            var imagePath = await _imageService.UploadImageAsync(ImageFile);
            var userId = _userManager.GetUserId(User);

            var image = new Image
            {
                FileName = imagePath,
                UploadedByUserId = userId ?? "unknown",
                Description = Description
            };

            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();

            UploadSuccess = true;
            return Page();
        }
    }
}