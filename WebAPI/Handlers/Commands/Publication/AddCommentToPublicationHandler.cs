using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Publication;

namespace WebAPI.Handlers.Commands.Publication;

public class AddCommentToPublicationHandler(CommentService service, UserService userService) : IRequestHandler<AddCommentToPublication>
{
	public async Task Handle(AddCommentToPublication request, CancellationToken cancellationToken)
	{
		var user = await userService.Get(request.UserId);
		var comment = new Domain.Entities.Comment()
		{
			Posted = request.CommentPosted,
			Text = request.CommentText,
			Issuer = user,
		};

		await service.AddCommentToPublication(request.PublicationId, comment);
	}
}
