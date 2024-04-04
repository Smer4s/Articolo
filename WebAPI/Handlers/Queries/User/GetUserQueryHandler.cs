using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Dto;
using WebAPI.Models.Queries.User;

namespace WebAPI.Handlers.Queries.User;

public class GetUserQueryHandler(UserService userService, IMapper mapper) : IRequestHandler<GetUserQuery, GetUserDto>
{
	public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var user = await userService.Get(request.Id);

		return mapper.Map<GetUserDto>(user);
	}
}
