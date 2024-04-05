using Client.Models;

namespace Client.Services.Abstractions;

public interface IAdminService
{
    Task<User[]?> GetUsers();
    Task<User[]?> GetModerators();
    Task AddModerator(int userId);
    Task RemoveModerator(int userId);
}
