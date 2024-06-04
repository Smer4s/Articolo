using Domain.Entities.Reactions;
using Domain.Entities;
using Domain.Enums;

namespace WebAPI.Models.Dto;

public record GetPublicationDto 
{
	public int Id { get; init; }
	public string Title { get; init; } = null!;
	public string Status { get; init; } = null!;
	public string XmlDocumentUrl { get; init; } = null!;
	public GetUserDto Issuer { get; init; } = null!;

	public IList<ThemeDto> Themes { get; init; } = null!;

	public IList<CommentDto>? Comments { get; init; } = null!;

	public IList<PublicationReactionDto>? Reactions { get; init; } = null!;

	public IList<string> Favorites { get; init; } = null!;

	public int FavouritesCount { get; init; }

	public DateTime Created { get; init; }

	public float Rating { get; init; }
}
