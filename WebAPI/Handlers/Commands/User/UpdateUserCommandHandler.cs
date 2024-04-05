using Domain.Models;
using Domain.Models.User;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.User;

namespace WebAPI.Handlers.Commands.User;

public class UpdateUserCommandHandler(UserService userService) : IRequestHandler<UpdateUserCommand>
{
	public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var dto = new UpdateUserDto()
		{
			Login = request.Login,
			BirthDay = request.BirthDay,
			Description = request.Description,
			Email = request.Email,
			Gender = request.Gender,
			Password = request.Password,
			UserName = request.UserName,
		};

		await userService.Update(request.Id, dto);
	}
}
