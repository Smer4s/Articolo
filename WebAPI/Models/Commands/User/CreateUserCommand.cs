using MediatR;

namespace WebAPI.Models.Commands.User;

public record CreateUserCommand : IRequest
{
	public string Login { get; init; } = null!;
	public string Password { get; init; } = null!;
}
