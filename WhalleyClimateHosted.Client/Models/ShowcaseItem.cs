

namespace WhalleyClimateHosted.Client.Models;

/// <summary>
/// A single tile displayed in the on-site progress showcase.
/// </summary>
public record ShowcaseItem(
    string ImageUrl,
    string AltText,
    string Heading,
    string Description);