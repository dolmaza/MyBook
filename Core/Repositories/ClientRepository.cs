using System.Data.Entity;

namespace Core.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {

    }

    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DbContext context)
            : base(context)
        {
        }
    }
}
