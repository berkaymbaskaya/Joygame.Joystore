﻿using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace Joygame.Joystore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        readonly private IProductService _productService;
        readonly private ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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

        [HttpGet("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productService.GetProductById(id);
            var categoryList = await _categoryService.GetCategory();

            if (!response.Success || response.Data == null)
            {
                return NotFound();
            }
            var product = response.Data;
            var model = new ProductEditModel
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
                UpdatedAt = product.UpdatedAt,
                CategorySelectList = categoryList.Data.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);
        }
        [HttpPost("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(ProductEditModel model)
        {
            var updateDto = model.ToUpdateDto();

            var response = await _productService.UpdateProduct(model.Id, updateDto);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Product Update failed.";
                return View(model);
            }

            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction("Index");

        }

        [HttpGet("Product/Create")]
        public async Task<IActionResult> Create()
        {
            var categoryList = await _categoryService.GetCategory();
            var model = new ProductCreateModel
            {
                CategorySelectList = categoryList.Data.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);
        }
        [HttpPost("Product/Create")]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var createDto = model.ToCreateDto();

            var response = await _productService.CreateProduct(createDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Create failed.");
                return View(model);
            }

            TempData["Success"] = "Product Created successfully.";
            return RedirectToAction("Index");

        }

        [HttpPost("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _productService.DeleteProduct(id);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Product deletion failed.";
                return View();
            }

            TempData["Success"] = "Product deleted successfully.";
            return RedirectToAction("Index");

        }



    }
}
