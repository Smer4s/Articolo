using Client.Models.Enums;

namespace Client.Models.Publication;

public record PublicationReaction
{
	public ReactionType ReactionType { get; init; }
	public int UserId { get; init; }
}
