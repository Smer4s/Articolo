namespace Domain.Entities.Reactions;

public class CommentReaction : Reaction
{
    public virtual Comment Comment { get; set; } = null!;
}
