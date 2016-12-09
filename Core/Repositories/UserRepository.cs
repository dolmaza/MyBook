using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string username, string passwordMd5);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public User Get(string username, string passwordMd5)
        {
            return GetAll()
                .Include(u => u.Role)
                .Include(u => u.Role.Permissions)
                .FirstOrDefault(u => u.Username == username && u.Password == passwordMd5);
        }
    }
}
