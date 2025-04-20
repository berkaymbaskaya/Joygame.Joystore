using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Joygame.Joystore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        readonly private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var response = await _productService.GetProduct(page, pageSize); 
            var viewModel = new ProductPagedResultViewModel
            {
                Items = response.Data.Items.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.CategoryName,
                    ParentCategoryName = p.ParentCategoryName,
                    ImageUrl = p.ImageUrl
                }).ToList(),
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = response.Data.TotalCount
            };

            return View(viewModel);
        }

    }
}
