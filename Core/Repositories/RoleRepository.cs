using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Permission> GetRolePermissions(int? ID);
    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Permission> GetRolePermissions(int? ID)
        {
            return GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.ID == ID)?.Permissions.ToList();
        }
    }
}
