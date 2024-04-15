using Client.Models.Publication;

namespace Client.Services.Abstractions;

public interface IPublicationService
{
    Task<Publication[]> GetPublications();
    Task<Publication?> GetPublication(int publicationId);
    Task CreatePublication(Publication publication);
    Task DeletePublication(int publicationId);
    Task UpdatePublication(Publication publication);
    Task AddPublicationToFavorites(int publicationId);
}
