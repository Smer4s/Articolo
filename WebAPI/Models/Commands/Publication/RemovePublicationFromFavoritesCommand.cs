using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record RemovePublicationFromFavoritesCommand : IRequest
{
    public int Id { get; init; }
    public int UserId { get; init; }
}
