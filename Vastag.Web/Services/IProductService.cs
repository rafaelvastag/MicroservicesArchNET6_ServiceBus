using ProductAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vastag.Web.Services
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDTO dto);
        Task<T> UpdateProductAsync<T>(ProductDTO dto);
        Task<T> DeleteProductAsync<T>(int id);
    }
}
