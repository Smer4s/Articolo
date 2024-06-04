using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class AddReactionToPublicationCommandHandler(PublicationService publicationService) : IRequestHandler<AddReactionToPublicationCommand>
{
	public async Task Handle(AddReactionToPublicationCommand request, CancellationToken cancellationToken)
	{
		await publicationService.AddReaction(request.PublicationId, request.UserId, request.ReactionType);
	}
}
