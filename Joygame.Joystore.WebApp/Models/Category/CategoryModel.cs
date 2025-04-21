using System.ComponentModel.DataAnnotations;

namespace Joygame.Joystore.WebApp.Models.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }

        public List<CategoryModel> Children { get; set; } = new(); 
    }

}

