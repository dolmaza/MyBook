using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetWithStatus(int? ID);
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context)
            : base(context)
        {
        }


        public Order GetWithStatus(int? ID)
        {
            return GetAll().Include(o => o.Status).AsNoTracking().SingleOrDefault(o => o.ID == ID);
        }
    }
}
