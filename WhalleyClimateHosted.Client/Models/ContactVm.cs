using System.ComponentModel.DataAnnotations;
namespace WhalleyClimateHosted.Client.Models;

public class ContactVm
{
    [Required, MinLength(2)]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, MinLength(4)]
    public string Subject { get; set; } = "";

    [Required, MinLength(10)]
    public string Message { get; set; } = "";
}