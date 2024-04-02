using Domain.Entities.Reactions;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public DateTime Posted { get; set; }

    public string Text { get; set; } = null!;

    public virtual User Issuer { get; set; } = null!;

    public virtual IList<CommentReaction>? Reactions { get; set; }
    public virtual Publication Publication { get; set; } = null!;
}
