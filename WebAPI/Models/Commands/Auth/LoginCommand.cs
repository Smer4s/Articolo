using Domain.Models.User;
using MediatR;

namespace WebAPI.Models.Commands.Auth;

public class LoginCommand : IRequest<ApiTokenModel>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
