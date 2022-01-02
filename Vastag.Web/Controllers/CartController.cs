using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await GetCartBasedOnLoggedUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDTO cart)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.ApplyCouponAsync<ResponseDTO>(cart, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return RedirectToAction(nameof(CartIndex));
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.RemoveCouponAsync<ResponseDTO>(userId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return RedirectToAction(nameof(CartIndex));
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

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCouponDetails<ResponseDTO>(cart.CartHeader.CouponCode,accessToken);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponResult = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                        cart.CartHeader.DiscountTotal = couponResult.DiscountAmount;
                    }
                }

                foreach (var item in cart.CartDetails)
                {
                    cart.CartHeader.OrderTotal += (item.Product.Price * item.Count);
                }
                cart.CartHeader.OrderTotal -= cart.CartHeader.DiscountTotal;
            }

            return cart;
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return RedirectToAction(nameof(CartIndex));
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await GetCartBasedOnLoggedUser());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartDTO cart)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.Checkout<ResponseDTO>(cart.CartHeader, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Confirmation));
            }
            return RedirectToAction(nameof(CartIndex));
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
    }
}
