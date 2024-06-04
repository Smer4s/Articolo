using Client.Models.Publication;

namespace Client.Extensions;

public static class ReactionExtensions
{
    public static IEnumerable<PublicationReaction> GetLikes(this IEnumerable<PublicationReaction> reactions) => 
        reactions.Where(x => x.ReactionType is Models.Enums.ReactionType.Like);
    public static IEnumerable<PublicationReaction> GetDisLikes(this IEnumerable<PublicationReaction> reactions) =>
        reactions.Where(x => x.ReactionType is Models.Enums.ReactionType.Dislike);
}
