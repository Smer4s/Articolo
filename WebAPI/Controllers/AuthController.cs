using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models.Commands.Auth;

namespace WebAPI.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> Auth(LoginCommand command)
    {
        var token = await mediator.Send(command);

        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var token = await mediator.Send(command);

        return Ok(token);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var strId = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) ?? throw new ArgumentNullException();

		var id = int.Parse(strId);

		await mediator.Send(new LogoutCommand()
        {
            Id = id
        });

        return Ok();
    }
}
