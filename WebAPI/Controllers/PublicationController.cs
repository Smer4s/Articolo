﻿using Domain.Extensions;
using Domain.Models.Publication;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Commands.Publication;
using WebAPI.Models.Queries.Publication;

namespace WebAPI.Controllers;

[ApiController]
[Route("/publication")]
public class PublicationController(IMediator mediator) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<IActionResult> GetPublication(int id)
	{
		var query = new GetPublicationQuery
		{
			Id = id
		};
		var publication = await mediator.Send(query);

		return Ok(publication);
	}

	[HttpGet]
	public async Task<IActionResult> GetPublications()
	{
		var publications = await mediator.Send(new GetPublicationsQuery());

		return Ok(publications);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreatePublication(CreatePublicationDto dto)
	{
		var command = new CreatePublicationCommand
		{
			UserId = User.GetId(),
			ThemeIds = dto.ThemeIds,
			Title = dto.Title,
			XmlDocument = dto.XmlDocument
		};

		await mediator.Send(command);

		return Ok();
	}

	[HttpPost("favorites/{id}")]
	[Authorize]
	public async Task<IActionResult> AddToFavorites(int id)
	{
		await mediator.Send(new AddPublicationToFavoritesCommand
		{
			Id = id,
			UserId = User.GetId(),
		});

		return Ok();
	}

	[HttpDelete("favorites/{id}")]
	[Authorize]
	public async Task<IActionResult> RemoveFromFavorites(int id)
	{
		await mediator.Send(new RemovePublicationFromFavoritesCommand
		{
			Id = id,
			UserId = User.GetId(),
		});

		return Ok();
	}

	[HttpPut]
	[Authorize]
	public async Task<IActionResult> EditMyPublication(UpdatePublicationDto dto)
	{
		var command = new UpdatePublicationCommand()
		{
			UserId = User.GetId(),
			Id = dto.Id,
            ThemeIds = dto.ThemeIds,
            Title = dto.Title,
            XmlDocument = dto.XmlDocument
        };
		await mediator.Send(command);

		return Ok();
	}

	[HttpDelete]
	[Authorize]
	public async Task<IActionResult> DeleteMyPublication()
	{
		var command = new DeletePublicationCommand
		{
			Id = User.GetId()
		};
		
		await mediator.Send(command);

		return Ok();
	}

	[HttpPost("like/{id}")]
	[Authorize]
	public async Task<IActionResult> LikePublication([FromRoute] int id)
	{
		var command = new AddReactionToPublicationCommand()
		{
			PublicationId = id,
			ReactionType = Domain.Enums.ReactionType.Like,
			UserId = User.GetId()
		};

		await mediator.Send(command);

		return Ok();
	}

	[HttpDelete("like/{id}")]
	[Authorize]
	public async Task<IActionResult> RemoveLikePublication([FromRoute] int id)
	{
		var command = new RemoveReactionFromPublicationCommand
		{
			PublicationId = id,
			ReactionType = Domain.Enums.ReactionType.Like,
			UserId = User.GetId()
		};

		await mediator.Send(command);

		return Ok();
	}

	[HttpPost("dislike/{id}")]
	[Authorize]
	public async Task<IActionResult> DislikePublication([FromRoute] int id)
	{
		var command = new AddReactionToPublicationCommand()
		{
			PublicationId = id,
			ReactionType = Domain.Enums.ReactionType.Dislike,
			UserId = User.GetId()
		};

		await mediator.Send(command);

		return Ok();
	}

	[HttpDelete("dislike/{id}")]
	[Authorize]
	public async Task<IActionResult> RemoveDislikePublication([FromRoute] int id)
	{
		var command = new RemoveReactionFromPublicationCommand
		{
			PublicationId = id,
			ReactionType = Domain.Enums.ReactionType.Dislike,
			UserId = User.GetId()
		};

		await mediator.Send(command);

		return Ok();
	}
}
