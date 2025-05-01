namespace WhalleyClimateHosted.Client.Models
{
    /// <summary>
    /// Represents one tab: controls hero + overview section.
    /// </summary>
    public record TabItem(
        string Title,
        string Subtitle,
        string HeroImageUrl,
        string ReadMoreUrl,
        string OverviewHeading,
        string OverviewText,
        string OverviewLink,
        string OverviewImageUrl
    );
}
