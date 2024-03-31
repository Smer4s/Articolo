using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(PostgreDbContext dbContext) : IUserRepository
    {
        public async Task<int> Create(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task Delete(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user is not null)
            {
                dbContext.Users.Remove(user);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<User?> Find(Func<User, bool> predicate) =>
            await Task.FromResult(dbContext.Users.Where(predicate).FirstOrDefault());

        public async Task<User?> Get(int id) =>
            await dbContext.Users.FindAsync(id);

        public async Task Update(int id, User updateUser)
        {
            dbContext.Users.Update(updateUser);

            await dbContext.SaveChangesAsync();
        }
    }
}
