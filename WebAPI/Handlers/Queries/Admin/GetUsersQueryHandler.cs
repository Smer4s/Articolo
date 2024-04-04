using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Dto;
using WebAPI.Models.Queries.Admin;

namespace WebAPI.Handlers.Queries.Admin;

public class GetUsersQueryHandler(AdminService adminService, IMapper mapper) : IRequestHandler<GetUsersQuery, IEnumerable<GetUserDto>>
{
    public async Task<IEnumerable<GetUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await adminService.GetUsers();

        return users.Select(mapper.Map<GetUserDto>);
    }
}
