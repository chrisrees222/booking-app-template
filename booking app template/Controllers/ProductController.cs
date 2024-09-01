using booking_app_template.Data;
using booking_app_template.Data.Services;
using booking_app_template.Data.Static;
using booking_app_template.Data.ViewModels;
using booking_app_template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace booking_app_template.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {
        
        private readonly IProductService _service;
        private readonly IOrderService _orderService;

        public ProductController(IProductService service, IOrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
                       
            var allProducts = await _service.GetAllAsync();            

            return View(allProducts);
        }

        // For searching movies, 
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                //var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new ProductOrderVM();
            model.Product = await _service.GetProductByIdAsync(id);
            model.OrderItems = await _orderService.GetOrderByProductId(id);
            model.DateList = model.DateList;
         
            // List to store DateOnly objects
            List<DateOnly> dateList = new List<DateOnly>();
            
            foreach (var item in model.OrderItems)
            {
                if (item.HireDay != null)
                {
                    model.DateList.Add(item.HireDay);                    
                }                           
            }    

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View();     
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            
            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                
            };
            
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            
            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //Get: product/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
