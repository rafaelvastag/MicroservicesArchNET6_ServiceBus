using ProductAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vastag.Web.Models;

namespace Vastag.Web.Services.Impl
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDTO dto)
        {
            return await this.SendAsync<T>( new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.POST,
                Data = dto,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.DELETE,
                Url = Constants.SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.GET,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.GET,
                Url = Constants.SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO dto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.PUT,
                Data = dto,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }
    }
}
