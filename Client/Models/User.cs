namespace Client.Models;

public record User
{
    public int Id { get; init; }
    public string? Login { get; init; }
    public string? UserName { get; init; }
    public string Role { get; init; } = null!;
    public DateTime? BirthDay { get; init; }
    public string? Email { get; init; }
    public string? Description { get; init; }
    public bool? Gender { get; init; }
}
