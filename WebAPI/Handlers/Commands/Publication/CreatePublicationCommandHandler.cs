using Domain.Entities;
using Domain.Models.Publication;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class CreatePublicationCommandHandler(PublicationService publicationService) : IRequestHandler<CreatePublicationCommand>
{
	public async Task Handle(CreatePublicationCommand request, CancellationToken cancellationToken)
	{
		var dto = new CreatePublicationDto
		{
			ThemeIds = request.ThemeIds,
			Title = request.Title,
			XmlDocument = request.XmlDocument,
		};
		await publicationService.Create(request.UserId, dto);
	}
}
