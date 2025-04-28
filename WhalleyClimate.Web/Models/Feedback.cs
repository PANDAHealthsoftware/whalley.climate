// File: Models/Feedback.cs
using System.ComponentModel.DataAnnotations;

namespace WhalleyClimate.Web.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public bool Approved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}