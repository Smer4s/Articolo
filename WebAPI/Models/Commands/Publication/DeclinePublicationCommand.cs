using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record DeclinePublicationCommand : IRequest
{
    public int Id { get; init; }
}
