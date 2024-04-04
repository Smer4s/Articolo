using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers.Queries.Publication;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Controllers;

[Authorize(Roles = "Moderator, Admin")]
[Route("/moder")]
[ApiController]
public class ModeratorController(IMediator mediator) : ControllerBase
{
    [HttpGet("publications")]
    public async Task<IActionResult> GetUnreviewedPublications()
    {
        var publications = await mediator.Send(new GetUnreviewedPublicationsCommand
        {

        });

        return Ok(publications);
    }

    [HttpPut("publication/approve/{id}")]
    public async Task<IActionResult> ApprovePublication(int id)
    {
        await mediator.Send(new ApprovePublicationCommand
        {
            Id = id
        });

        return Ok();
    }

    [HttpPut("publication/decline/{id}")]
    public async Task<IActionResult> DeclinePublication(int id)
    {
        await mediator.Send(new DeclinePublicationCommand
        {
            Id = id
        });

        return Ok();
    }
}
