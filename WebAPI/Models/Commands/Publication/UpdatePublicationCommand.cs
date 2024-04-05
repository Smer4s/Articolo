using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record UpdatePublicationCommand : IRequest
{
    public int UserId { get; init; }
    public int Id { get; init; }
	public IList<int> ThemeIds { get; init; } = null!;
	public string Title { get; init; } = null!;
	public string XmlDocument { get; init; } = null!;
}
