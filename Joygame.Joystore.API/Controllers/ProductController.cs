using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Joygame.Joystore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetPagedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize);
            var response = new ApiResponse<PagedResult<ProducViewtDto>>
            {
                Data = new PagedResult<ProducViewtDto>
                {
                    Items = result.Items,
                    TotalCount = result.TotalCount
                },
                Success = true
            };
            return Ok(response);
        }
    }
}
