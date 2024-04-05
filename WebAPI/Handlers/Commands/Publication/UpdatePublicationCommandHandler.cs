using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class UpdatePublicationCommandHandler(PublicationService publicationService) : IRequestHandler<UpdatePublicationCommand>
{
    public async Task Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        await publicationService.Update(new Domain.Models.Publication.UpdatePublicationDto()
        {
            Id = request.Id,
            ThemeIds = request.ThemeIds,
            Title = request.Title,
            XmlDocument = request.XmlDocument,
        });
    }
}
