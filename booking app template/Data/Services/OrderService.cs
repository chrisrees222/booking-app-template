using booking_app_template.Data.Static;
using booking_app_template.Data.ViewModels;
using booking_app_template.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.Xml;


namespace booking_app_template.Data.Services
{
    public class _ordersService : IOrderService
    {
        private readonly AppDbContext _context;

        public _ordersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n => n.User).OrderByDescending(n => n).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

       

        public async Task<List<OrderItem>> GetOrderByProductId(int id)
        {
            var orderItems = await _context.OrderItems.ToListAsync();

            orderItems = orderItems.Where(n => n.ProductId == id).ToList();

            return orderItems;                                     
        }



        public async Task StoreOrder(List<ShoppingCartItem> items, string UserId, string userEmailAddress, string checkOutId)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = userEmailAddress,
                CheckoutId = checkOutId
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price,
                    HireDay = item.DateOnly
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        
    }
}
