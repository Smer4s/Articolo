using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Handlers.Queries.Publication;
using WebAPI.Models.Dto;

namespace WebAPI.Handlers.Commands.Publication;

public class GetUnreviewedPublicationsCommandHandler(PublicationService publicationService, IMapper mapper) : IRequestHandler<GetUnreviewedPublicationsCommand, IEnumerable<GetPublicationDto>>
{
    public async Task<IEnumerable<GetPublicationDto>> Handle(GetUnreviewedPublicationsCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(publicationService.GetUnreviewedPublications().Select(mapper.Map<GetPublicationDto>));
    }
}
