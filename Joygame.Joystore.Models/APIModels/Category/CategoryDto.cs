namespace Joygame.Joystore.API.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int Depth { get; set; }
    }
}
