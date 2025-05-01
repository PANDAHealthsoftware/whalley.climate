using System.ComponentModel.DataAnnotations;
namespace WhalleyClimateHosted.Client.Models;

public class ContactVm
{
    [Required, StringLength(80)]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string? Phone { get; set; }

    [Required, StringLength(2000)]
    public string Message { get; set; } = "";
}