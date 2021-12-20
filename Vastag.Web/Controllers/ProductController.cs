using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vastag.Web.Models.DTOs;
using Vastag.Web.Services;

namespace Vastag.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetAllProductsAsync<ResponseDTO>(accessToken);

            if (null != response && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDTO dto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.CreateProductAsync<ResponseDTO>(dto, accessToken);

            if (null != response && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDTO>(id, accessToken);

            if (null != response && response.IsSuccess)
            {
                ProductDTO p = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(p);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDTO dto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.UpdateProductAsync<ResponseDTO>(dto, accessToken);

            if (null != response && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(dto);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDTO dto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDTO>(dto.Id, accessToken);

            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDTO>(id, accessToken);

            if (null != response && response.IsSuccess)
            {
                ProductDTO p = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(p);
            }

            return NotFound();
        }

    }
}
