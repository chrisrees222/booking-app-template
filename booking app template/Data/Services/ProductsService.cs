using booking_app_template.Data.Base;
using booking_app_template.Data.ViewModels;
using booking_app_template.Models;
using Microsoft.EntityFrameworkCore;

namespace booking_app_template.Data.Services
{
    public class ProductsService: EntityBaseRepository<Product>, IProductService
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL
                
                
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();      
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
            
            .FirstOrDefaultAsync(n=>n.Id == id);

            return productDetails;
        }


        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                
                await _context.SaveChangesAsync();
            }
                        
            await _context.SaveChangesAsync();
        }
    }
}
