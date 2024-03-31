using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Get(int id);
        Task<int> Create(User user);
        Task Update(int id, User updateUser);
        Task Delete(int id);
        Task<User?> Find(Func<User, bool> predicate);
    }
}
