using Domain.Models;
using Domain.Services;
using MediatR;
using WebAPI.Models.Commands.Auth;

namespace WebAPI.Handlers.Commands.Auth
{
    public class LoginCommandHandler(AuthService authService) : IRequestHandler<LoginCommand, ApiTokenModel>
    {
        public async Task<ApiTokenModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await authService.GetToken(new LoginModel()
            {
                Login = request.Login,
                Password = request.Password,
            });

            return token;
        }
    }
}
