using Domain.Entities;

namespace Infrastructure.Repositories;

public abstract class Repository<T>(PostgreDbContext dbContext) where T : BaseEntity
{
	public async Task Create(T entity)
	{
		dbContext.Set<T>().Add(entity);

		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(int id)
	{
		var entity = dbContext.Set<T>().Find(id);

		if (entity is not null)
		{
			dbContext.Set<T>().Remove(entity);

			await dbContext.SaveChangesAsync();
		}
	}
	public async Task<T?> Find(Func<T, bool> predicate) =>
		await Task.FromResult(dbContext.Set<T>().Where(predicate).FirstOrDefault());

	public async Task<T?> Get(int id) =>
		await dbContext.Set<T>().FindAsync(id);

	public Task SaveChanges() => dbContext.SaveChangesAsync();
}