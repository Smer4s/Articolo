using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Commands.Auth;

namespace WebAPI.Controllers
{
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
    }
}
