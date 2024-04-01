using Client.Models.Auth;

namespace Client.Services.Abstractions;

public interface IAuthenticationService
{
    Task Authenticate(AuthCredentials credentials);
}
