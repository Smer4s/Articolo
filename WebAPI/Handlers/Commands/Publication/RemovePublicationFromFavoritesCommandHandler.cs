using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class RemovePublicationFromFavoritesCommandHandler(PublicationService service) : IRequestHandler<RemovePublicationFromFavoritesCommand>
{
	public async Task Handle(RemovePublicationFromFavoritesCommand request, CancellationToken cancellationToken)
	{
		await service.RemovePublicationFromFavorites(request.Id, request.UserId);
	}
}
