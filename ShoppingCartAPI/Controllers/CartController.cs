using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models.DTOs;
using ShoppingCartAPI.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDTO _response;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDTO();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDTO CartDTO = await _cartRepository.GetCartByUserId(userId);
                _response.Result = CartDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDTO CartDTO)
        {
            try
            {
                CartDTO cartDt = await _cartRepository.CreateUpdateCart(CartDTO);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO CartDTO)
        {
            try
            {
                CartDTO cartDt = await _cartRepository.CreateUpdateCart(CartDTO);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("RemoveCart/{cartId}")]
        public async Task<object> RemoveCart(int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.Remove(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}