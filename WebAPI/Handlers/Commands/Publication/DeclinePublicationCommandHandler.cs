using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class DeclinePublicationCommandHandler(PublicationService publicationService) : IRequestHandler<DeclinePublicationCommand>
{
    public async Task Handle(DeclinePublicationCommand request, CancellationToken cancellationToken)
    {
        await publicationService.DeclinePublication(request.Id);
    }
}
