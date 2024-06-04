using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record DeletePublicationCommand : IRequest
{
	public int Id { get; init; }
}
