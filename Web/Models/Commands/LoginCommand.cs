using Domain.Models;
using MediatR;

namespace Web.Models.Commands;

public class LoginCommand : IRequest<ApiToken?>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
