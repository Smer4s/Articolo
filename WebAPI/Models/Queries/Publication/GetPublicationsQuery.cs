using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Queries.Publication;
public record GetPublicationsQuery : IRequest<IEnumerable<GetPublicationDto>>
{ }
