using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class AuthService(IUserRepository userRepository)
    {
        public async Task<ApiToken?> GetToken(LoginModel loginModel)
        {
            var user = await userRepository.Find(u => u.UserName == loginModel.Login && u.Password == loginModel.Password);


            if (user is not null)
            {
                var token = new ApiToken()
                {
                    AccessToken = "AccessToken",
                    RefreshToken = "RefreshToken"
                };

                return token;
            }

            return null;
        }
    }
}
