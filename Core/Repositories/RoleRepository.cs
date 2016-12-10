using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<int?> GetRolePermissions(int? ID);
        void UpdateRolePermissions(int? roleID, ICollection<Permission> permissions);
    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<int?> GetRolePermissions(int? ID)
        {
            return GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.ID == ID)?.Permissions.Select(r => r.ID).ToList();
        }

        public void UpdateRolePermissions(int? roleID, ICollection<Permission> permissions)
        {
            var role = GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.ID == roleID);

            if (role != null)
            {
                role.Permissions.Where(w => !permissions.Contains(w)).ToList().ForEach(permission =>
                {
                    role.Permissions.Remove(permission);
                });

                permissions?.Where(w => !role.Permissions.Contains(w)).ToList().ForEach(permission =>
                {
                    role.Permissions.Add(permission);
                });
            }
        }
    }
}
