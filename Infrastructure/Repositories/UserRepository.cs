using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class UserRepository(PostgreDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{ }
