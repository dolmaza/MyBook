using System.Data.Entity;

namespace Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {

    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }
    }
}
