using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Auth;

namespace WebAPI.Handlers.Commands.Auth
{
    public class LogoutCommandHandler(AuthService authService) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await authService.Logout(request.Id);
        }
    }
}
