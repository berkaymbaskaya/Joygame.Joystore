using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Helpers;
using Joygame.Joystore.WebApp.Models.Category;
using Joygame.Joystore.WebApp.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Joygame.Joystore.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetCategory();
            if (response.Data == null || !response.Data.Any())
            {
                return View(new CategoryModel { });
            }
            var flatCategories = response.Data.Select(p => new CategoryModel
            {
                Id = p.Id,
                Name = p.Name,
                ParentId = p.ParentId,
                ParentName = p.ParentName,
            }).ToList();
            var tree = CategoryHelper.BuildCategoryTree(flatCategories); 
            return View(tree);
        }
    }
}
