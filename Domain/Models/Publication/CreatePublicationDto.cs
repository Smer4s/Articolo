namespace Domain.Models.Publication;

public record CreatePublicationDto
{
	public string Title { get; init; } = null!;
	public string XmlDocument { get; init; } = null!;
	public IList<int> ThemeIds { get; init; } = null!;
}
