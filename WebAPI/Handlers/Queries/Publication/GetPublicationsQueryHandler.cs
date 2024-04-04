using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Dto;
using WebAPI.Models.Queries.Publication;

namespace WebAPI.Handlers.Queries.Publication;

public class GetPublicationsQueryHandler(PublicationService publicationService, IMapper mapper) : IRequestHandler<GetPublicationsQuery, IEnumerable<GetPublicationDto>>
{
    public async Task<IEnumerable<GetPublicationDto>> Handle(GetPublicationsQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(publicationService.GetApprovedPublications().Select(mapper.Map<GetPublicationDto>));
    }
}
