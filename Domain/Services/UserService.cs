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

        public async Task<User> Get(int id) => 
            await userRepository.Get(id) ?? throw new Exception();

        public async Task Update(int id, User updateUser) =>
            await userRepository.Update(id, updateUser);
    }
}
