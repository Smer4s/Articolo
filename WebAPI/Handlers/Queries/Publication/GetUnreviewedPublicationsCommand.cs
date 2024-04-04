using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Handlers.Queries.Publication;

public record GetUnreviewedPublicationsCommand : IRequest<IEnumerable<GetPublicationDto>>
{ }
