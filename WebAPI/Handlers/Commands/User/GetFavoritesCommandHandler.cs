using AutoMapper;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.User;
using WebAPI.Models.Dto;

namespace WebAPI.Handlers.Commands.User;

public class GetFavoritesCommandHandler(UserService userService, IMapper mapper) : IRequestHandler<GetFavoritesCommand, IEnumerable<GetPublicationDto>>
{
    public async Task<IEnumerable<GetPublicationDto>> Handle(GetFavoritesCommand request, CancellationToken cancellationToken)
    {
        var pubs = await userService.GetFavorites(request.Id);

        return pubs.Select(mapper.Map<GetPublicationDto>);
    }
}
