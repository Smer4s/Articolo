using MediatR;

namespace WebAPI.Models.Commands.Auth;

public record LogoutCommand : IRequest
{
    public int Id { get; set; }
}
