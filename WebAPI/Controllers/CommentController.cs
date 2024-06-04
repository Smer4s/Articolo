using Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Commands.Publication;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers;


[ApiController]
[Route("/comment")]
public class CommentController(IMediator mediator) : ControllerBase
{
	[Authorize]
	[HttpPost]
	public async Task<IActionResult> AddComment(AddCommentDto dto)
	{
		var command = new AddCommentToPublication()
		{
			PublicationId = dto.PublicationId,
			UserId = User.GetId(),
			CommentPosted = dto.CommentPosted,
			CommentText = dto.CommentText,
		};

		await mediator.Send(command);

		return Ok();
	}
}
