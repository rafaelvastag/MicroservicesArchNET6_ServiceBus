using System.Threading.Tasks;
using Vastag.Web.Models.DTOs;

namespace Vastag.Web.Services
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDTO dto, string token);
        Task<T> UpdateProductAsync<T>(ProductDTO dto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
