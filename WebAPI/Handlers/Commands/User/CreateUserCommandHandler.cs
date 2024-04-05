using Domain.Models;
using Domain.Models.User;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.User;

namespace WebAPI.Handlers.Commands.User;

public class CreateUserCommandHandler(UserService userService) : IRequestHandler<CreateUserCommand>
{
	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var loginModel = new LoginModel
		{
			Login = request.Login,
			Password = request.Password
		};

		await userService.Create(loginModel);
	}
}
