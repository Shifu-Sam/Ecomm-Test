using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task PlaceOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task ProcessPaymentAsync(int orderId, string paymentStatus);
        Task DeleteOrderAsync(int id);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);

    }
}
