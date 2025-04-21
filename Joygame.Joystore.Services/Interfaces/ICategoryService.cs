using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Category;
using Joygame.Joystore.API.Models.Product;
using Refit;

namespace Joygame.Joystore.Services.Interfaces
{
    public interface ICategoryService
    {
        [Get("/Category")]
        Task<API.Core.ApiResponse<List<CategoryDto>>> GetCategory();


    }
}
