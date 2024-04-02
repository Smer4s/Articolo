using Domain.Entities.Reactions;
using Domain.Enums;

namespace Domain.Entities;
public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Login { get; set; } = null!; 
    public Role Role { get; set; }
    public virtual IList<Publication>? Publications { get; set; }
    public virtual IList<Publication>? Favorites { get; set; }
    public virtual IList<Comment>? Comments { get; set; }
    public virtual IList<Reaction>? Reactions { get; set; }
}
