using Microsoft.EntityFrameworkCore;
using Ordering.Core.Contracts;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
            return orderList;
        }
    }
}
