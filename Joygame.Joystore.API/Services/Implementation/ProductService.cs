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
        public async Task<PagedResult<ProductViewtDto>> GetPagedProducts(int pageNumber, int pageSize)
        {
            var result = new PagedResult<ProductViewtDto>();

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetProductsWithCategory";
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "@PageNumber";
            param1.Value = pageNumber;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "@PageSize";
            param2.Value = pageSize;
            command.Parameters.Add(param2);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Items.Add(new ProductViewtDto
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CategoryId = reader.GetInt32(2),
                    ParentCategoryId = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    CategoryName = reader.GetString(4),
                    ParentCategoryName = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Price = reader.GetDecimal(6),
                    ImageUrl = reader.IsDBNull(7) ? null : reader.GetString(7)
                });
            }

            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.TotalCount = reader.GetInt32(0);
            }

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
