using System.Threading.Tasks;
using Vastag.Web.Models.DTOs;

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
