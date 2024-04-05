using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetFavorites(int id);
    Task<IList<User>> GetUsers(Func<User, bool>? predicate = null);
}
