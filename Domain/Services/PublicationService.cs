using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Entities.Reactions;
using Domain.Enums;
using Domain.Models.Publication;
using Domain.Repositories;

namespace Domain.Services;

public class PublicationService(UserService users, IPublicationRepository publications, IThemeRepository themes)
{
	public async Task<Publication> GetPublication(int id)
	{
		var publ = await publications.GetPublication(id);

		Guard.Against.NotFound(id, publ);

		return publ;
	}
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
			Status = Status.Moderation,
			Created = DateTime.Now
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
		publication.Rating = 3.0F;

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

	public async Task RemovePublicationFromFavorites(int id, int userId)
	{
		var user = await users.Get(userId);
		var publication = publications.GetAll(x => x.Id == id).First();

		if (publication.Favourites is null)
			throw new Exception("Wrong publication");

		publication.Favourites.Remove(user);

		if (user.Favorites is null)
			throw new Exception("Wrong user");

		user.Favorites.Remove(publication);

		await users.Update(user);
		await publications.SaveChanges();
	}

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

	public async Task AddReaction(int publicationId, int userId, ReactionType reactionType)
	{
		var publication = await publications.GetPublication(publicationId);
		var user = await users.Get(userId);

		Guard.Against.NotFound(publicationId, publication);

		if (publication.Reactions is null)
			publication.Reactions = [];

		var publicationReaction = new PublicationReaction()
		{
			Issuer = user,
			ReactionType = reactionType,
			Publication = publication,
		};

		publication.Reactions.Add(publicationReaction);

		switch (publicationReaction.ReactionType)
		{
			case ReactionType.Like:
				IncreaseRating(publication);
				break;
			case ReactionType.Dislike:
				DecreaseRating(publication);
				break;
		}

		await publications.SaveChanges();
	}
	public async Task RemoveReaction(int publicationId, int userId, ReactionType reactionType)
	{
		var publication = await publications.GetPublication(publicationId);
		var user = await users.Get(userId);

		Guard.Against.NotFound(publicationId, publication);
		if (publication.Reactions is null) return;

		var reaction = publication.Reactions.First(x =>
		x.Publication == publication &&
		x.Issuer == user &&
		x.ReactionType == reactionType);

		publication.Reactions.Remove(reaction);

		switch (reaction.ReactionType)
		{
			case ReactionType.Like:
				DecreaseRating(publication);
				break;
			case ReactionType.Dislike:
				IncreaseRating(publication);
				break;
		}

		await publications.SaveChanges();
	}

	private static void IncreaseRating(Publication publication)
	{
		publication.Rating += 0.1F;
		if (publication.Rating > 5.0F)
			publication.Rating = 5.0F;
	}

	private static void DecreaseRating(Publication publication)
	{
		publication.Rating -= 0.1F;
		if (publication.Rating < 1.0F)
			publication.Rating = 1.0F;
	}
}
