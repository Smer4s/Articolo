namespace Client.Models.Comment;

public record Comment
{
    public int Id { get; init; }

    public DateTime Posted { get; init; }

    public string Text { get; init; } = null!;

    public string IssuerName { get; init; } = null!;

    public IList<CommentReaction>? Reactions { get; set; }
}
