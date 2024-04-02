using Domain.Enums;

namespace Domain.Entities.Reactions;

public abstract class Reaction : BaseEntity
{
    public ReactionType ReactionType { get; set; }

    public virtual User Issuer { get; set; } = null!;
}
