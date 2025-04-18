using AutoMapper;
using Joygame.Joystore.API.Contexts;
using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Entities;
using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Joygame.Joystore.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PagedResult<ProductViewDto>> GetPagedProducts(int pageNumber, int pageSize)
        {
            var result = new PagedResult<ProductViewDto>();

            // 1. Data from SP
            var data = await _context.ProductViewDto
                .FromSqlRaw("EXEC sp_GetProductsWithCategory @PageNumber = {0}, @PageSize = {1}", pageNumber, pageSize)
                .ToListAsync();

            result.Items = data;

            // 2. Total count: EF LINQ 
            result.TotalCount = await _context.Products
                .Where(p => p.IsDeleted == false && p.IsActive == true)
                .Join(_context.Categories,
                      p => p.CatId,
                      c => c.Id,
                      (p, c) => new { Product = p, Category = c })
                .Where(x => x.Category.IsDeleted == false && x.Category.IsActive == true)
                .CountAsync();

            return result;
        }

        public async Task<int> CreateProduct(ProductCreateRequestDto req)
        {
            try
            {
                var product = _mapper.Map<Product>(req);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
            catch
            {
                throw;
            }

        }
        public async Task UpdateProduct(int id, ProductUpdateRequestDto req)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null || product.IsDeleted == true)
                {
                    throw new AppExceptions.RecordNotFoundException("Product not found");
                }

                _mapper.Map(req, product);
                product.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }
        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null || product.IsDeleted == true)
                {
                    throw new AppExceptions.RecordNotFoundException("Product not found");
                }
                product.IsDeleted = true;
                product.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProductDetailDto> GetProductDetail(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                var productDetail = _mapper.Map<ProductDetailDto>(product);
                return productDetail;
            }
            catch
            {
                throw;
            }
        }

    }
}
