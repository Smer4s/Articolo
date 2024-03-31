using Domain.Models;
using Domain.Services;
using MediatR;
using Web.Models.Commands;

namespace Web.Handlers.Commands
{
    public class LoginCommandHandler(AuthService authService) : IRequestHandler<LoginCommand, ApiToken?>
    {
        public async Task<ApiToken?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await authService.GetToken(new Domain.Models.LoginModel()
            {
                Login = request.Login,
                Password = request.Password,
            });

            return token;
        }
    }
}
