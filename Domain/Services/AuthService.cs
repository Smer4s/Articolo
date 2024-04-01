using Ardalis.GuardClauses;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class AuthService(IUserRepository userRepository)
    {
        public async Task<ApiTokenModel> GetToken(LoginModel loginModel)
        {
            var user = await userRepository.Find(u => u.UserName == loginModel.Login && u.Password == loginModel.Password);


            if (user is null)
            {
                Guard.Against.NotFound(loginModel.Login, user);
            }

            var token = new ApiTokenModel()
            {
                AccessToken = "AccessToken",
                RefreshToken = "RefreshToken"
            };

            return token;
        }

        public async Task<ApiTokenModel> RefreshToken(ApiTokenModel apiToken)
        {
            return await Task.FromResult(new ApiTokenModel()
            {
                AccessToken = "new" + apiToken.AccessToken,
                RefreshToken = "new" + apiToken.RefreshToken,
            });
        }
    }
}
