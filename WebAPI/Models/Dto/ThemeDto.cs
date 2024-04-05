namespace WebAPI.Models.Dto;

public record ThemeDto 
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
}
