using Joygame.Joystore.API.Contexts;
using Joygame.Joystore.API.Models.Category;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Joygame.Joystore.API.Services.Implementation
{
    public class CategoryService: ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CategoryDto> GetCategories()
        {
            var response= _context.RecursiveCategories.FromSqlRaw("EXEC sp_GetRecursiveCategories").ToList();
            return response;
        }
    }
}
