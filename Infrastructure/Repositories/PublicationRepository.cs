using Domain.Entities;
using Domain.Repositories;
using System.Linq;

namespace Infrastructure.Repositories;

public class PublicationRepository(PostgreDbContext dbContext) : Repository<Publication>(dbContext), IPublicationRepository
{
	public IList<Publication> GetAll(Func<Publication, bool>? filter = null)
	{
		if (filter is null)
		{
			return dbContext.Publications.ToList();
		}

		return dbContext.Publications.Where(filter).ToList();
	}
}
