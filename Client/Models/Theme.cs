namespace Client.Models;

public record Theme
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}
