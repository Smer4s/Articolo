namespace Client.Models.Publication;

public class Publication
{
    public int Id { get; set; }
    public User Issuer { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Content { get; set; } = null!;

    public IList<Theme> Themes { get; set; } = null!;

    public IList<Comment.Comment>? Comments { get; set; } = null!;

    public IList<PublicationReaction>? Reactions { get; set; } = null!;
    public DateTime Created { get; set; }

    public int FavouritesCount { get; set; }

    public float Rating { get; set; }
}
