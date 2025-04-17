using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResult<ProducListViewtDto>> GetPagedProducts(int pageNumber, int pageSize);
        Task<int> CreateProduct(ProductCreateDto dto);
        Task UpdateProduct(int id,ProductUpdateDto dto);
        //Task<bool> DeleteProductAsync(int id);


    }
}
