using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Admin;

namespace WebAPI.Handlers.Commands.Admin;

public class AddModeratorCommandHandler(AdminService service) : IRequestHandler<AddModeratorCommand>
{
    public async Task Handle(AddModeratorCommand request, CancellationToken cancellationToken)
    {
        await service.MakeModerator(request.UserId);
    }
}
