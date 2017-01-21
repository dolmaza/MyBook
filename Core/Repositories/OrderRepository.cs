using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetWithStatus(int? ID);
        Order GetOrderPaper(int? ID);
        void OrdersArchive(List<int?> orderIDs);
        void OrdersUnArchive(List<int?> orderIDs);
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

        public Order GetOrderPaper(int? ID)
        {
            return GetAll()
                .Include(o => o.Status)
                .Include(o => o.OrderDetails)
                .AsNoTracking()
                .SingleOrDefault(o => o.ID == ID);
        }

        public void OrdersArchive(List<int?> orderIDs)
        {
            var orders = GetAll().Where(p => orderIDs.Contains(p.ID ?? 0)).ToList();

            orders.ForEach(o =>
            {
                o.IsArchived = true;
                Update(o);
            });

        }
        public void OrdersUnArchive(List<int?> orderIDs)
        {
            var orders = GetAll().Where(p => orderIDs.Contains(p.ID ?? 0)).ToList();

            orders.ForEach(o =>
            {
                o.IsArchived = false;
                Update(o);
            });

        }
    }
}
