using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Dto;
using WebAPI.Models.Queries.Admin;

namespace WebAPI.Handlers.Queries.Admin;

public class GetModeratorsQueryHandler(AdminService adminService, IMapper mapper) : IRequestHandler<GetModeratorsQuery, IEnumerable<GetUserDto>> 
{
    public async Task<IEnumerable<GetUserDto>> Handle(GetModeratorsQuery request, CancellationToken cancellationToken)
    {
        var moders = await adminService.GetModerators();

        return moders.Select(mapper.Map<GetUserDto>);
    }
}
