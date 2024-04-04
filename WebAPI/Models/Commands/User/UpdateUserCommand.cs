using MediatR;

namespace WebAPI.Models.Commands.User;

public record UpdateUserCommand : IRequest
{
	public int Id { get; init; }
	public string? UserName { get; init; } = null!;
	public string? Password { get; init; } = null!;
	public string? Login { get; init; } = null!;
	public DateTime? BirthDay { get; init; }
	public string? Email { get; init; }
	public string? Description { get; init; }
	public bool? Gender { get; init; }
}
