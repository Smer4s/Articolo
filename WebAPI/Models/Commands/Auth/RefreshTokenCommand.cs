using Domain.Models;
using MediatR;

namespace WebAPI.Models.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<ApiTokenModel>
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
