namespace Domain.Entities.Reactions;

public class PublicationReaction : Reaction
{
    public virtual Publication Publication { get; set; } = null!;
}
