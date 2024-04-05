using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Models.User;
using Domain.Repositories;
using System.Reflection;
using System.Xml;

namespace Domain.Services
{
    public class UserService(HashService hashService, IUserRepository userRepository, IMapper mapper)
	{
		public async Task Create(LoginModel loginModel)
		{
			var password = hashService.HashPassword(loginModel.Password);

			var user = new User
			{
				Login = loginModel.Login,
				Password = password,
			};

			await userRepository.Create(user);
		}

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

		public async Task Update(int id, UpdateUserDto dto)
		{
			var user = await Get(id);

			user.Login = dto.Login ?? user.Login;
			user.Password = dto.Password ?? user.Password;

			user.UserName = dto.UserName;
			user.Email = dto.Email;
			user.Gender = dto.Gender;
			user.Description = dto.Description;
			user.BirthDay = dto.BirthDay;
		}

		public async Task<IEnumerable<Publication>> GetFavorites(int id)
		{
			var user = await userRepository.GetFavorites(id);

			Guard.Against.NotFound(id, user);

			return user.Favorites ?? [];
		}
	}
}
