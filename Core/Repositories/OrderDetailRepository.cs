using System.Data.Entity;

namespace Core.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {

    }

    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DbContext context)
            : base(context)
        {
        }
    }
}
