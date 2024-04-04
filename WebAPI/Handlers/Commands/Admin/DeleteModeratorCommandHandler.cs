using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Admin;

namespace WebAPI.Handlers.Commands.Admin;

public class DeleteModeratorCommandHandler(AdminService service) : IRequestHandler<DeleteModeratorCommand>
{
    public async Task Handle(DeleteModeratorCommand request, CancellationToken cancellationToken)
    {
        await service.RemoveModerator(request.UserId);
    }
}
