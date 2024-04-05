namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; } = null!;
    public virtual IList<Theme> Themes { get; set; } = null!;
}
