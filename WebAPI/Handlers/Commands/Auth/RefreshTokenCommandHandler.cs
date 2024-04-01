using Domain.Models;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Auth;

namespace WebAPI.Handlers.Commands.Auth
{
    public class RefreshTokenCommandHandler(AuthService authService) : IRequestHandler<RefreshTokenCommand, ApiTokenModel>
    {
        public Task<ApiTokenModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return authService.RefreshToken(new ApiTokenModel()
            {
                AccessToken = request.AccessToken,
                RefreshToken = request.RefreshToken,
            });
        }
    }
}
