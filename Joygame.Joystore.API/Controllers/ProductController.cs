using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Joygame.Joystore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize(Roles = "product_view")]
        [HttpGet()]
        public async Task<IActionResult> GetPagedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetPagedProducts(pageNumber, pageSize);
            var response = new ApiResponse<PagedResult<ProductViewtDto>>
            {
                Data = new PagedResult<ProductViewtDto>
                {
                    Items = result.Items,
                    TotalCount = result.TotalCount
                },
                Success = true
            };
            return Ok(response);
        }

        [Authorize(Roles = "product_update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateRequestDto req)
        {
            await _productService.UpdateProduct(id, req);
            return NoContent();
        }

        [Authorize(Roles = "product_creator")]
        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequestDto req)
        {
            var result = await _productService.CreateProduct(req);
            var response = new ApiResponse<int>
            {
                Data = result,
                Success = true
            };
            return Ok(response);
        }

        [Authorize(Roles = "product_delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }

        [Authorize(Roles = "product_view")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var result = await _productService.GetProductDetail(id);
            var response = new ApiResponse<ProductDetailDto>
            {
                Data = result,
                Success = true
            };
            return Ok(response);
        }

    }
}
