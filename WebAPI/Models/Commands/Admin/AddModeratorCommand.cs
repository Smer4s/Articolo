using MediatR;

namespace WebAPI.Models.Commands.Admin;

public record AddModeratorCommand : IRequest
{
    public int UserId { get; init; }
}
