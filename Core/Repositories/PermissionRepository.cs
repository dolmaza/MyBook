using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IQueryable<Permission> GetMenuItems();
    }

    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbContext context)
            : base(context)
        {
        }

        public IQueryable<Permission> GetMenuItems()
        {
            return GetAll().Where(p => p.IsMenuItem);
        }
    }
}
