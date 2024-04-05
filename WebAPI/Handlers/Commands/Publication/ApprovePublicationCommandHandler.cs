using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class ApprovePublicationCommandHandler(PublicationService publicationService) : IRequestHandler<ApprovePublicationCommand>
{
    public async Task Handle(ApprovePublicationCommand request, CancellationToken cancellationToken)
    {
        await publicationService.ApprovePublication(request.Id);
    }
}
