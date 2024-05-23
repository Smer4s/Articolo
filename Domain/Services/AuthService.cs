using Ardalis.GuardClauses;
using Domain.Exceptions;
using Domain.Models.User;
using Domain.Repositories;

namespace Domain.Services
{
    public class AuthService(HashService hash, UserService users, TokenService tokenService)
    {
        public async Task<ApiTokenModel> GetToken(LoginModel loginModel)
        {
            var user = await users.Find(user => user.Login == loginModel.Login && user.Password == hash.HashPassword(loginModel.Password));

            var tokens = tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;

            await users.Update(user);

            return tokens;
        }

        public async Task<ApiTokenModel> RefreshToken(ApiTokenModel apiToken)
        {
            var id = TokenService.GetIdFromTokenString(apiToken.AccessToken);

            var user = await users.Get(id);

            var tokens = tokenService.RefreshTokens(user, apiToken.RefreshToken, apiToken.AccessToken);

            user.RefreshToken = tokens.RefreshToken;

            await users.Update(user);

            return tokens;
        }

        public async Task Logout(int id)
        {
            var user = await users.Get(id);

            user.RefreshToken = null;

            await users.Update(user);
        }
    }
}
