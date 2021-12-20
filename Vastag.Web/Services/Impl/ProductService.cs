using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vastag.Web.Models;
using Vastag.Web.Models.DTOs;

namespace Vastag.Web.Services.Impl
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDTO dto, string token)
        {
            return await this.SendAsync<T>( new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.POST,
                Data = dto,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.DELETE,
                Url = Constants.SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.GET,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.GET,
                Url = Constants.SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO dto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.PUT,
                Data = dto,
                Url = Constants.SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
