using Domain.Enums;
using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record AddReactionToPublicationCommand : IRequest
{
	public int PublicationId { get; set; }
	public int UserId { get; set; }
	public ReactionType ReactionType { get; set; }
}
