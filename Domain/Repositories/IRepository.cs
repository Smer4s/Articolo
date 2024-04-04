using Domain.Entities;

namespace Domain.Repositories;

public interface IRepository<T> where T : BaseEntity
{
	Task<T?> Get(int id);
	Task Create(T user);
	Task Delete(int id);
	Task SaveChanges();
	Task<T?> Find(Func<T, bool> predicate);
}
