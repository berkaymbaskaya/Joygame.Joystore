﻿using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpGet()]
        public async Task<IActionResult> GetPagedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetPagedProducts(pageNumber, pageSize);
            var response = new ApiResponse<PagedResult<ProducListViewtDto>>
            {
                Data = new PagedResult<ProducListViewtDto>
                {
                    Items = result.Items,
                    TotalCount = result.TotalCount
                },
                Success = true
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody] ProductUpdateDto req)
        {
            await _productService.UpdateProduct(id, req);
            return NoContent();
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto req)
        {
            var result = await _productService.CreateProduct(req);
            var response = new ApiResponse<int>
            {
                Data = result,
                Success = true
            };
            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }


    }
}
