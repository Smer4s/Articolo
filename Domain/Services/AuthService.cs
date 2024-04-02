using Ardalis.GuardClauses;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class AuthService(UserService users, TokenService tokenService)
    {
        public async Task<ApiTokenModel> GetToken(LoginModel loginModel)
        {
            var user = await users.Find(u => u.Login == loginModel.Login && u.Password == loginModel.Password);

           return tokenService.GenerateTokens(user);
        }

        public async Task<ApiTokenModel> RefreshToken(ApiTokenModel apiToken)
        {
            return await Task.FromResult(new ApiTokenModel()
            {
                AccessToken = "new" + apiToken.AccessToken,
                RefreshToken = "new" + apiToken.RefreshToken,
            });
        }

        public async Task Logout(int id)
        {
            var user = await users.Get(id);

            user.RefreshToken = null;

            await users.Update(user);
        }
    }
}
