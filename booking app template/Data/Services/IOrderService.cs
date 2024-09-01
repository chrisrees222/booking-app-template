using booking_app_template.Models;
using Microsoft.EntityFrameworkCore;

namespace booking_app_template.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrder(List<ShoppingCartItem> items, string userId, string EmalAddress, string checkOutId);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);

        Task<List<OrderItem>> GetOrderByProductId(int id);
    }
}
