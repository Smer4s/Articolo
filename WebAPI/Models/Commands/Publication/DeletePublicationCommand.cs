using MediatR;

namespace WebAPI.Models.Commands.Publication;

public class DeletePublicationCommand : IRequest
{
	public int Id { get; init; }
}
