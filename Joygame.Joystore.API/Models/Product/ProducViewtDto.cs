namespace Joygame.Joystore.API.Models.Product
{
    public class ProducViewtDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

    }
}
