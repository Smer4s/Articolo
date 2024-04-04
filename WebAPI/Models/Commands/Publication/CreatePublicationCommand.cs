using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record CreatePublicationCommand : IRequest
{
	public int UserId { get; init; }
	public string Title { get; init; } = null!;
	public string XmlDocument { get; init; } = null!;
	public IList<int> ThemeIds { get; init; } = null!;
}
