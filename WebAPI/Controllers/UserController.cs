using Domain.Extensions;
using Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebAPI.Handlers.Commands.User;
using WebAPI.Models.Commands.User;
using WebAPI.Models.Queries.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IMediator mediator) : ControllerBase
{
	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetUser()
	{
		var query = new GetUserQuery()
		{
			Id = User.GetId()
		};

		var user = await mediator.Send(query);

		return Ok(user);
	}

	[HttpPost]
	public async Task<IActionResult> Register(CreateUserCommand command)
	{
		await mediator.Send(command);

		return Ok();
	}

	[Authorize]
	[HttpPut]
	public async Task<IActionResult> UpdateUser(UpdateUserDto model)
	{
		var command = new UpdateUserCommand()
		{
			Id = User.GetId(),
			Gender = model.Gender,
			BirthDay = model.BirthDay,
			Description = model.Description,
			Email = model.Email,
			Login = model.Login,
			Password = model.Password,
			UserName = model.UserName
		};

		await mediator.Send(command);

		return Ok();
	}

	[Authorize]
	[HttpGet("favs")]
	public async Task<IActionResult> GetFavorites()
	{
		var pubs = await mediator.Send(new GetFavoritesCommand
		{
			Id = User.GetId(),
		});

		return Ok(pubs);
	}
}
