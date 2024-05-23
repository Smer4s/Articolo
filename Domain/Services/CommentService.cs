using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services;

public class CommentService(PublicationService service,ICommentRepository repository)
{
	public async Task AddCommentToPublication(int publicationId, Comment comment)
	{
		comment.Publication = await service.GetPublication(publicationId);

		await repository.Create(comment);
	}
}
