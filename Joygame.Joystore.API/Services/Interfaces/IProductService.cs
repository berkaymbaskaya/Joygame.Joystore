using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Product;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResult<ProducViewtDto>> GetPagedProductsAsync(int pageNumber, int pageSize);

    }
}
