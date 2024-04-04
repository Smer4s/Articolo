using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record ApprovePublicationCommand : IRequest
{
    public int Id { get; init; }
}
