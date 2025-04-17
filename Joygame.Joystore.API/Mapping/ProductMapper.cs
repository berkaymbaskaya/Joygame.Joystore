using AutoMapper;
using Joygame.Joystore.API.Entities;
using Joygame.Joystore.API.Models.Product;
namespace Joygame.Joystore.API.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductCreateDto, Product>();

        }
    }
}
