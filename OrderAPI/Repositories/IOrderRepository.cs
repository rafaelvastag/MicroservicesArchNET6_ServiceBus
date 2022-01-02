using OrderAPI.Models;
using System.Threading.Tasks;

namespace OrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader order);
        Task UpdateOrderPaymentStatus(int orderId, bool paid);
    }
}
