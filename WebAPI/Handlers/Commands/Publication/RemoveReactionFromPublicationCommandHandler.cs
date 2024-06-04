using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class RemoveReactionFromPublicationCommandHandler(PublicationService publicationService) : IRequestHandler<RemoveReactionFromPublicationCommand>
{
	public async Task Handle(RemoveReactionFromPublicationCommand request, CancellationToken cancellationToken)
	{
		await publicationService.RemoveReaction(request.PublicationId, request.UserId, request.ReactionType);
	}
}
