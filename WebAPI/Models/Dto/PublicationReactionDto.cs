using Domain.Enums;

namespace WebAPI.Models.Dto;

public record PublicationReactionDto
{
	public int Id { get; init; }
	public ReactionType ReactionType { get; init; }
	public int UserId { get; init; }
}
