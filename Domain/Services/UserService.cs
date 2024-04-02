using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using System.Reflection;

namespace Domain.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper)
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

        public async Task<User> Find(Func<User, bool> predicate)
        {
            var user = await userRepository.Find(predicate);

			Guard.Against.NotFound("login", user);

			return user;
		}
        public async Task Update(User updateUser)
        {
            var user = await Get(updateUser.Id);

            mapper.Map(updateUser, user);

            await userRepository.SaveChanges();
        }
    }
}
