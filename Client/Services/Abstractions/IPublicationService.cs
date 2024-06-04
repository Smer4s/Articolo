using Client.Models.Comment;
using Client.Models.Publication;

namespace Client.Services.Abstractions;

public interface IPublicationService
{
    Task<Publication[]> GetPublications();
    Task<Publication?> GetPublication(int publicationId);
	Task AddCommentToPublication(int publicationId, Comment comment);
    Task CreatePublication(Publication publication);
    Task DeletePublication(int publicationId);
    Task UpdatePublication(Publication publication);
    Task AddPublicationToFavorites(int publicationId);
	Task RemovePublicationFromFavorites(int publicationId);
	Task LikePublication(int publicationId);
	Task RemoveLikeFromPublication(int publicationId);
	Task DislikePublication(int publicationId);
	Task RemoveDislikeFromPublication(int publicationId);
}
