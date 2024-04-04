
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services;

public class AdminService(IUserRepository users)
{
    public Task<IList<User>> GetModerators() =>
        users.GetUsers(x => x.Role is Enums.Role.Moderator);

    public Task<IList<User>> GetUsers()=>
        users.GetUsers(x=>x.Role is Enums.Role.Default);

    public async Task MakeModerator(int userId)
    {
        var user = await users.Get(userId);

        Guard.Against.NotFound(userId, user);

        user.Role = Enums.Role.Moderator;

        await users.SaveChanges();
    }

    public async Task RemoveModerator(int userId)
    {
        var user = await users.Get(userId);

        Guard.Against.NotFound(userId, user);

        user.Role = Enums.Role.Default;

        await users.SaveChanges();
    }
}
