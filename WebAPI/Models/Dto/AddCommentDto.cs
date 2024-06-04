namespace WebAPI.Models.Dto;

public record AddCommentDto
{
	public required int PublicationId { get; set; }
	public string CommentText { get; set; } = null!;
	public DateTime CommentPosted { get; set; }
}
