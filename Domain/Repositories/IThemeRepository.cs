using Domain.Entities;

namespace Domain.Repositories;

public interface IThemeRepository : IRepository<Theme>
{
	public IList<Theme> GetThemes(Func<Theme,bool>? filter = null);
}
