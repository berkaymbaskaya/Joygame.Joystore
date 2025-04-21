namespace Joygame.Joystore.API.Models.Product
{
    public class BaseProductModel
    {
        public string? Name { get; set; }
        public int? CatId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }

    }
}
