using Client.Models.Enums;

namespace Client.Models.Publication;

public record PublicationReaction
{
    public int Id { get; init; }
    public ReactionType ReactionType { get; init; }
}
