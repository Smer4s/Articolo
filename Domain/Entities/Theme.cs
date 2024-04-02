using System.ComponentModel;

namespace Domain.Entities;

public class Theme : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual Category Category { get; set; } = null!; 
    public virtual IList<Publication>? Publications { get; set; }
}
