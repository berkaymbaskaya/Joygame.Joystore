using Joygame.Joystore.API.Models.Category;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategories();

    }
}
