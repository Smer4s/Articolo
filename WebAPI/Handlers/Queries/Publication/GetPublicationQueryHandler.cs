using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Dto;
using WebAPI.Models.Queries.Publication;

namespace WebAPI.Handlers.Queries.Publication;

public class GetPublicationQueryHandler(IMapper mapper, PublicationService publicationService) : IRequestHandler<GetPublicationQuery, GetPublicationDto>
{
	public async Task<GetPublicationDto> Handle(GetPublicationQuery request, CancellationToken cancellationToken)
	{
		var publ = await publicationService.Get(request.Id);

		return mapper.Map<GetPublicationDto>(publ);
	}
}
