namespace Domain.Models.User;
public record LoginModel
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}
