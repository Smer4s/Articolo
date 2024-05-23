using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories;

public class PublicationRepository(PostgreDbContext dbContext) : Repository<Publication>(dbContext), IPublicationRepository
{
	public IList<Publication> GetAll(Func<Publication, bool>? filter = null)
	{
		var pubs = dbContext.Publications.Include(x => x.Favourites)
			.Include(x => x.Reactions);

		if (filter is null)
		{
			return pubs.ToList();
		}

		return pubs.Where(filter).ToList();
	}

	public async Task<Publication?> GetPublication(int id) =>
		await dbContext.Publications.Include(x => x.Comments)
		.ThenInclude(x => x.Issuer)
		.Where(x => x.Id == id)
		.FirstOrDefaultAsync();
}
