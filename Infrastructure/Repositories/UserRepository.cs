using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(PostgreDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public Task<User?> GetFavorites(int id) => dbContext.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IList<User>> GetUsers(Func<User, bool>? predicate = null)
    {
        if (predicate is null) return await dbContext.Users.ToListAsync();

        return await Task.FromResult(dbContext.Users.Where(predicate).ToList());
    }
}
