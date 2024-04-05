using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class AddPublicationToFavoritesCommandHandler(PublicationService publicationService) : IRequestHandler<AddPublicationToFavoritesCommand>
{
    public async Task Handle(AddPublicationToFavoritesCommand request, CancellationToken cancellationToken)
    {
       await publicationService.AddPublicationToFavorites(request.Id, request.UserId);
    }
}
