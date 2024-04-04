using Domain.Entities.Reactions;
using Domain.Entities;

namespace WebAPI.Models.Dto;

public record CommentDto
{
	public int Id { get; init; }

	public DateTime Posted { get; init; }

	public string Text { get; init; } = null!;

	public string IssuerName { get; init; } = null!;

	public IList<CommentReaction>? Reactions { get; set; }
}
