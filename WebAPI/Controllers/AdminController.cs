using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Commands.Admin;
using WebAPI.Models.Queries.Admin;

namespace WebAPI.Controllers;

[Route("/admin")]
[Authorize(Roles = "Admin")]
[ApiController]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpPost("moder/{userId}")]
    public async Task<IActionResult> AddModerator(int userId)
    {
        await mediator.Send(new AddModeratorCommand
        {
            UserId = userId
        });

        return Ok();
    }

    [HttpDelete("moder/{userId}")]
    public async Task<IActionResult> DeleteModerator(int userId)
    {
        await mediator.Send(new DeleteModeratorCommand
        {
            UserId = userId
        });

        return Ok();
    }

    [HttpGet("moders")]
    public async Task<IActionResult> GetModerators()
    {
        var moders = await mediator.Send(new GetModeratorsQuery());

        return Ok(moders);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await mediator.Send(new GetUsersQuery());

        return Ok(users);
    }
}
