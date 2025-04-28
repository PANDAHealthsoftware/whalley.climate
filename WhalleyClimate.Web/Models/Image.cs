// File: Models/Image.cs
using System.ComponentModel.DataAnnotations;

namespace WhalleyClimate.Web.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? Description { get; set; }

        public string UploadedByUserId { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}