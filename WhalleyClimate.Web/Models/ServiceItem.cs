// File: Models/ServiceItem.cs
using System.ComponentModel.DataAnnotations;

namespace WhalleyClimate.Web.Models
{
    public class ServiceItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string? IconUrl { get; set; }
    }
}