using Joygame.Joystore.API.Contexts;
using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Joygame.Joystore.API.Services.Implementation
{
    public class ProductService:IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ProducViewtDto>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            var result = new PagedResult<ProducViewtDto>();

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
                result.Items.Add(new ProducViewtDto
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

    }
}
