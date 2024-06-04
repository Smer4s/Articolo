using Domain.Entities.Reactions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class User : BaseEntity
{
	public string? UserName { get; set; }
	public string Password { get; set; } = null!;
    public string Login { get; set; } = null!;
	public DateTime? BirthDay { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public bool? Gender { get; set; }
	public Role Role { get; set; }
	public string? RefreshToken { get; set; }
    public virtual IList<Publication>? Publications { get; set; }
    public virtual IList<Publication>? Favorites { get; set; }
    public virtual IList<Comment>? Comments { get; set; }
    public virtual IList<Reaction>? Reactions { get; set; }
}
