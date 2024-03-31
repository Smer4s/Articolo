namespace Domain.Models;

public class ApiToken
{
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;
}
