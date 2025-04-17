using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResult<ProducListViewtDto>> GetPagedProducts(int pageNumber, int pageSize);
        //Task<int> CreateProductAsync(ProductCreateDto dto);
        //Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto);
        //Task<bool> DeleteProductAsync(int id);


    }
}
