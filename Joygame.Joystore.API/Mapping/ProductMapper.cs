using AutoMapper;
using Joygame.Joystore.API.Entities;
using Joygame.Joystore.API.Models.Product;
namespace Joygame.Joystore.API.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductUpdateRequestDto, Product>();
            CreateMap<ProductCreateRequestDto, Product>();
            CreateMap<Product, ProductDetailDto>();


        }
    }
}
