using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                Items = response.Data.Items.Select(p => new ProductListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.CategoryName,
                    ParentCategoryName = p.ParentCategoryName,
                    ImageUrl = p.ImageUrl,
                    IsActive=p.isActive
                }).ToList(),
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = response.Data.TotalCount
            };

            return View(viewModel);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetProduct(int id)
        //{
        //    var response = await _productService.GetProductById(id);
        //    if (response.Data == null)
        //        return NotFound();
        //    var product = response.Data;
        //    var viewModel = new ProductDetailModel
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Price = product.Price,
        //        CatId = product.CatId,
        //        ImageUrl = product.ImageUrl,
        //        Description = product.Description
        //    };
        //    return Json(viewModel);
        //}

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _productService.GetProductById(id);

            if (!response.Success || response.Data == null)
            {
                return NotFound();
            }
            var product = response.Data;
            var model = new ProductDetailModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ParentCategoryId = product.ParentCategoryId,
                CategoryName = product.CategoryName,
                ParentCategoryName = product.ParentCategoryName,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
            return View(model);
        }



    }
}
