using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Extensions;
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
        await mediator.Send(new LogoutCommand()
        {
            Id = User.GetId()
        });

        return Ok();
    }
}
