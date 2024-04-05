using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Commands.User;

public record GetFavoritesCommand : IRequest<IEnumerable<GetPublicationDto>>
{
    public int Id { get; init; }
}
