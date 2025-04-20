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
    public interface IProductSerive
    {
        [Get("/Product")]
        Task<API.Core.PagedResult<ProductViewDto>> Product(int pageNumber,int pageSize);

    }
}
