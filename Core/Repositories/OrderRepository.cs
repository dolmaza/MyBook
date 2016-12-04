using System.Data.Entity;

namespace Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {

    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context)
            : base(context)
        {
        }
    }
}
