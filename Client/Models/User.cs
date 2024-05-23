using Client.Models.Enums;

namespace Client.Models;

public record User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? UserName { get; set; }
    public string Role { get; set; } = null!;
    public DateTime? BirthDay { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public bool? Gender { get; set; }
	public Gender GenderEnum
	{
		get
		{
			return Gender.FromBool();
		}
		set
		{
			Gender = value.FromGender();
		}
	}
}
