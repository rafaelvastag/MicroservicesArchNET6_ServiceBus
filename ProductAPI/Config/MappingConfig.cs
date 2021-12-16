using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.DTOs;

namespace ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductEntity, ProductDTO>();
                config.CreateMap<ProductDTO, ProductEntity>();
            });
            return mappingConfig;
        }
    }
}
