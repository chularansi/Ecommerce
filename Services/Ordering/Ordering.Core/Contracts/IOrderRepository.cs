using Ordering.Core.Entities;

namespace Ordering.Core.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
