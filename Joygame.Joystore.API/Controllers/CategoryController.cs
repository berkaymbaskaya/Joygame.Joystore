using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Category;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Joygame.Joystore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _categoryService.GetCategories();
                var response = new ApiResponse<List<CategoryDto>>
                {
                    Data = categories,
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                var response = new ApiResponse<string>
                {
                    Data = null,
                    Success = false,
                    Error = new Error
                    {
                        Message = "An error occurred while retrieving categories.",
                        Code = "500"
                    }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
