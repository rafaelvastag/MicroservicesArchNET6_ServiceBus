using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vastag.Web.Models.DTOs;
using Vastag.Web.Services;

namespace Vastag.Web.Controllers
{
    public class CartController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await GetCartBasedOnLoggedUser());
        }

        private async Task<CartDTO> GetCartBasedOnLoggedUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);

            CartDTO cart = new();

            if (response != null && response.IsSuccess)
            {
                cart = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }
            if (cart.CartHeader != null)
            {
                foreach (var item in cart.CartDetails)
                {
                    cart.CartHeader.OrderTotal += (item.Product.Price * item.Count);
                }
            }

            return cart;
        }
    }
}
