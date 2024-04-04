using MediatR;

namespace WebAPI.Models.Commands.Admin;

public record DeleteModeratorCommand : IRequest
{
    public int UserId { get; init; }
}
