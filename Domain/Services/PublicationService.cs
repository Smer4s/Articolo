using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Publication;
using Domain.Repositories;

namespace Domain.Services;

public class PublicationService(UserService users, IPublicationRepository publications, IThemeRepository themes)
{
	public async Task CreatePublication(int id, CreatePublicationDto dto)
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

	public async Task<Publication> GetPublication(int id)
	{
		var publication = await publications.Get(id);

		Guard.Against.NotFound(id, publication);

		return publication;
	}

	public IList<Publication> GetPublications()
	{
		return publications.GetAll();
	}
}
