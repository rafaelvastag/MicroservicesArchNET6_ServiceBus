using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vastag.Web.Models.DTOs;

namespace Vastag.Web.Services
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null);
        Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartId, string token = null);
    }
}
