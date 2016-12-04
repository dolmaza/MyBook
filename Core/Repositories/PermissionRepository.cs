using System.Data.Entity;

namespace Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {

    }

    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
