using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class CommentRepository(PostgreDbContext dbContext) : Repository<Comment>(dbContext), ICommentRepository
{
}
