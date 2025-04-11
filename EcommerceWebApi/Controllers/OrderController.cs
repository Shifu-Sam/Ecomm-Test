using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        public OrdersController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(Order order)
        {
            await _orderRepo.PlaceOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderID }, order);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            await _orderRepo.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpPut("{id}/payment")]
        public async Task<ActionResult> ProcessPayment(int id, [FromBody] string paymentStatus)
        {
            await _orderRepo.ProcessPaymentAsync(id, paymentStatus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderRepo.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}
