using Client.Models.Publication;

namespace Client.Services.Abstractions;

public interface IModeratorService
{
    Task<Publication[]> GetPublications();
    Task ApprovePublication(int publicationId);
    Task DeclinePublication(int publicationId);
}
