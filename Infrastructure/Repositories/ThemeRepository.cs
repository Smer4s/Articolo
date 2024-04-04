using Domain.Entities;
using Domain.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ThemeRepository(PostgreDbContext dbContext) : Repository<Theme>(dbContext), IThemeRepository
{
	public IList<Theme> GetThemes(Func<Theme, bool>? filter = null)
	{
		if (filter is null)
		{
			return dbContext.Themes.ToList();
		}

		return dbContext.Themes.Where(filter).ToList();
	}
}
