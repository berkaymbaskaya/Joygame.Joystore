using Joygame.Joystore.API.Models.Category;
using Joygame.Joystore.API.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joygame.Joystore.WebApp.Models.Product
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductPagedResultViewModel
    {
        public List<ProductListViewModel> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
    public class ProductDetailModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public ProductUpdateRequestDto ToUpdateDto()
        {
            return new ProductUpdateRequestDto
            {
                Id=this.Id,
                Name = this.Name,
                Price = this.Price ?? 0,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
                CatId = this.CategoryId,
                IsActive = this.IsActive
            };
        }

    }

    public class ProductEditModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
    }
    public class ProductCreateModel
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
        public ProductCreateRequestDto ToCreateDto()
        {
            return new ProductCreateRequestDto
            {
                Name = this.Name,
                Price = this.Price ?? 0,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
                CatId = this.CategoryId,
                IsActive = this.IsActive
            };
        }
    }
}

