namespace Domain.Models.User;

public record UpdateUserDto
{
    public string? UserName { get; init; } = null!;
    public string? Password { get; init; } = null!;
    public string? Login { get; init; } = null!;
    public DateTime? BirthDay { get; init; }
    public string? Email { get; init; }
    public string? Description { get; init; }
    public bool? Gender { get; init; }
}
