using AutoMapper;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Models.DTOs;

namespace ShoppingCartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDTO>().ReverseMap();
                config.CreateMap<Cart, CartDTO>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
