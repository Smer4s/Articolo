using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class UserService(IUserRepository userRepository)
    {
        public async Task<int> Create(User user) =>
            await userRepository.Create(user);

        public async Task Delete(int id) => 
            await userRepository.Delete(id);

        public async Task<User> Get(int id)
        {
            var user = await userRepository.Get(id);

            Guard.Against.NotFound(id, user);

            return user;
        }
            
        public async Task Update(int id, User updateUser) =>
            await userRepository.Update(id, updateUser);
    }
}
