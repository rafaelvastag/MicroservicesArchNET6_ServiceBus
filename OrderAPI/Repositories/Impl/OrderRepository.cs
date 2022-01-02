using Microsoft.EntityFrameworkCore;
using OrderAPI.Context;
using OrderAPI.Models;
using System;
using System.Threading.Tasks;

namespace OrderAPI.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DbContextOptions<ApplicationDbContext> _context;

        public OrderRepository(DbContextOptions<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader order)
        {
            await using var _db = new ApplicationDbContext(_context);
            _db.OrderHeaders.Add(order);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderId, bool paid)
        {
            await using var _db = new ApplicationDbContext(_context);
            var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderId);

            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
        }
    }
}
