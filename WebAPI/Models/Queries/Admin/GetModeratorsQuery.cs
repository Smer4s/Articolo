using MediatR;
using WebAPI.Models.Dto;

namespace WebAPI.Models.Queries.Admin;

public record GetModeratorsQuery : IRequest<IEnumerable<GetUserDto>>
{ }
