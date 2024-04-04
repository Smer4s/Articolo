using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Queries.User;

public record GetUserQuery : IRequest<GetUserDto>
{
	public int Id { get; init; }
}
