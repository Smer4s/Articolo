using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Queries.Admin;

public class GetUsersQuery : IRequest<IEnumerable<GetUserDto>>
{ }
