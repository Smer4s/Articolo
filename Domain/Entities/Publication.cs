using Domain.Entities.Reactions;
using Domain.Enums;

namespace Domain.Entities;

public class Publication : BaseEntity
{
    public Status Status { get; set; }

    public string Title { get; set; } = null!;

    public virtual User Issuer { get; set; } = null!;

    public string XmlDocumentUrl { get; set; } = null!;

    public virtual IList<Theme> Themes { get; set; } = null!;

    public virtual IList<Comment>? Comments { get; set; } = null!;

    public virtual IList<PublicationReaction>? Reactions { get; set; } = null!;

    public virtual IList<User>? Favourites { get; set; } = null!;
	public DateTime Created { get; set; }

	public float Rating { get; set; }
}
