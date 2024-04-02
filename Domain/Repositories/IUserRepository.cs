using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Get(int id);
        Task<int> Create(User user);
        Task Delete(int id);
        Task SaveChanges();
        Task<User?> Find(Func<User, bool> predicate);
    }
}
