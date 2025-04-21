using Joygame.Joystore.API.Models.Category;
using Joygame.Joystore.API.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Joygame.Joystore.WebApp.Models.Product
{
    public class BaseProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProductListViewModel: BaseProductModel
    {
    }

    public class ProductPagedResultViewModel
    {
        public List<ProductListViewModel> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
    public class ProductDetailModel: BaseProductModel
    {
        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }

    public class ProductEditModel: BaseProductModel
    {

        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
        public ProductUpdateRequestDto ToUpdateDto()
        {
            return new ProductUpdateRequestDto
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price ?? 0,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
                CatId = this.CategoryId,
                IsActive = this.IsActive
            };
        }
    }
    public class ProductCreateModel:BaseProductModel
    {
        public int? CategoryId { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
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

