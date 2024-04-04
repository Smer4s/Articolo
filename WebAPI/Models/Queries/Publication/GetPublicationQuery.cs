using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Queries.Publication;

public record GetPublicationQuery : IRequest<GetPublicationDto>
{
	public int Id { get; init; }
}
