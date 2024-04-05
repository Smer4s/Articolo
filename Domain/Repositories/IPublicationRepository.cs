using Domain.Entities;

namespace Domain.Repositories;

public interface IPublicationRepository : IRepository<Publication>
{
	public IList<Publication> GetAll(Func<Publication,bool>? filter = null);
}
