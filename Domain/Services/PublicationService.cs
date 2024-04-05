using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Publication;
using Domain.Repositories;

namespace Domain.Services;

public class PublicationService(UserService users, IPublicationRepository publications, IThemeRepository themes)
{
    public async Task Create(int id, CreatePublicationDto dto)
    {
        var user = await users.Get(id);
        var theme = themes.GetThemes(x => dto.ThemeIds.Contains(x.Id));

        var publication = new Publication()
        {
            Issuer = user,
            Title = dto.Title,
            XmlDocumentUrl = dto.XmlDocument,
            Themes = theme,
            Status = Status.Moderation
        };

        await publications.Create(publication);
    }

    public async Task Update(UpdatePublicationDto dto)
    {
        var pub = await Get(dto.Id);

        pub.XmlDocumentUrl = dto.XmlDocument;
        pub.Title = dto.Title;

        await publications.SaveChanges();
    }

    public async Task<Publication> Get(int id)
    {
        var publication = await publications.Get(id);

        Guard.Against.NotFound(id, publication);

        return publication;
    }

    public IEnumerable<Publication> GetPublications() =>
        publications.GetAll();

    public IEnumerable<Publication> GetApprovedPublications() =>
        publications.GetAll(x => x.Status is Status.Published);

    public async Task ApprovePublication(int id)
    {
        var publication = await Get(id);

        publication.Status = Status.Published;

        await publications.SaveChanges();
    }

    public async Task DeclinePublication(int id)
    {
        var publication = await Get(id);

        publication.Status = Status.Denied;

        await publications.SaveChanges();
    }

    public Task Delete(int id) => publications.Delete(id);

    public IEnumerable<Publication> GetUnreviewedPublications() =>
        publications.GetAll(x => x.Status is Status.Moderation);

    public async Task AddPublicationToFavorites(int id, int userId)
    {
        var user = await users.Get(userId);
        var publication = await Get(id);

        if (publication.Favourites is null)
        {
            publication.Favourites = [user];
        }
        else
        {
            publication.Favourites.Add(user);
        }

        if (user.Favorites is null)
        {
            user.Favorites = [publication];
        }
        else
        {
            user.Favorites.Add(publication);
        }

        await users.Update(user);
        await publications.SaveChanges();
    }
}
