using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.Product;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joygame.Joystore.Services.Interfaces
{
    public interface IProductService
    {
        [Get("/Product")]
        Task<API.Core.ApiResponse<PagedResult<ProductViewDto>>> GetProduct(int pageNumber,int pageSize);

    }
}
