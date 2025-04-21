using Joygame.Joystore.API.Models.Product;

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

    }

}
