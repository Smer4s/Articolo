using Domain.Enums;
using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record RemoveReactionFromPublicationCommand : IRequest
{
	public int UserId { get; set; }
	public int PublicationId { get; set; }
	public ReactionType ReactionType { get; set; }
}
