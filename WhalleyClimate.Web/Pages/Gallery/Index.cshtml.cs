using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhalleyClimate.Web.Data;
using WhalleyClimate.Web.Models;

namespace WhalleyClimate.Web.Pages.Gallery
{
    public class IndexModel : PageModel
    {
        private readonly WhalleyClimateDbContext _dbContext;

        public IndexModel(WhalleyClimateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Image> Images { get; set; } = new();

        public async Task OnGetAsync()
        {
            Images = await _dbContext.Images
                .OrderByDescending(i => i.UploadedAt)
                .ToListAsync();
        }
    }
}