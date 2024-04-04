namespace Domain.Models.Publication;

public record UpdatePublicationDto
{
    public int Id { get; init; }
    public IList<int> ThemeIds { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string XmlDocument { get; init; } = null!;
}
