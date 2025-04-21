namespace Joygame.Joystore.API.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ParentName { get; set; }

        public int? ParentId { get; set; }
    }
}
