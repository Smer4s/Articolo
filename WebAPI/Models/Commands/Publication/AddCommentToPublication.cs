using MediatR;

namespace WebAPI.Models.Commands.Publication;

public record AddCommentToPublication : IRequest
{
	public required int PublicationId { get; set; }
	public required int UserId { get; set; }
	public string CommentText { get; set; } = null!;
	public DateTime CommentPosted {  get; set; }
}
