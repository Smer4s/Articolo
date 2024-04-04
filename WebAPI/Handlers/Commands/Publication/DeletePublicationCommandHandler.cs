using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class DeletePublicationCommandHandler(PublicationService publicationService) : IRequestHandler<DeletePublicationCommand>
{
    public async Task Handle(DeletePublicationCommand request, CancellationToken cancellationToken)
    {
        await publicationService.Delete(request.Id);
    }
}
