using Client.Models;
using Client.Models.Auth;
using Client.Models.Publication;

namespace Client.Services.Abstractions;

public interface IUserService
{
    Task<User> GetUser();
    Task RegisterUser(AuthCredentials credentials);
    Task UpdateUser(User user);
    Task<IEnumerable<Publication>> GetFavorites();
	Task Logout();
}
