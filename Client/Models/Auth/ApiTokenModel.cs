namespace Client.Models.Auth;

public class ApiTokenModel
{
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;
}
