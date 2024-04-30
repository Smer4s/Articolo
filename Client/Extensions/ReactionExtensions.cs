using Client.Models.Publication;

namespace Client.Extensions;

public static class ReactionExtensions
{
    public static int GetLikes(this IEnumerable<PublicationReaction> reactions) => 
        reactions.Where(x => x.ReactionType is Models.Enums.ReactionType.Like).Count();
    public static int GetDisLikes(this IEnumerable<PublicationReaction> reactions) =>
        reactions.Where(x => x.ReactionType is Models.Enums.ReactionType.Dislike).Count();
}
