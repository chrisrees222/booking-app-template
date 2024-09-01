 using booking_app_template.Data.Base;
using booking_app_template.Data.ViewModels;
using booking_app_template.Models;

namespace booking_app_template.Data.Services
{
    public interface IProductService: IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);

        Task AddNewProductAsync(NewProductVM data);

        Task UpdateProductAsync(NewProductVM product);
    }
}
