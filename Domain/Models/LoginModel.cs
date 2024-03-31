namespace Domain.Models;
public record LoginModel
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}
