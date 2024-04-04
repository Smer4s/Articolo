using Client.Models.Auth;

namespace Client.Services.Abstractions;

public interface IAuthenticationService
{
    Task<ApiTokenModel> Authenticate(AuthCredentials credentials);
}
