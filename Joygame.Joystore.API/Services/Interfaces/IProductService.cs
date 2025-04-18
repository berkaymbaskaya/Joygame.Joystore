using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResult<ProductViewDto>> GetPagedProducts(int pageNumber, int pageSize);
        Task<int> CreateProduct(ProductCreateRequestDto dto);
        Task UpdateProduct(int id,ProductUpdateRequestDto dto);
        public Task DeleteProduct(int id);
        public Task<ProductDetailDto> GetProductDetail(int id);

    }
}
