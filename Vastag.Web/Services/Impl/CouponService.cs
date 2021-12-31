using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vastag.Web.Models;

namespace Vastag.Web.Services.Impl
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetCouponDetails<T>(string couponCode, string token = null)
        {

            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.SD.ApiType.GET,
                Url = Constants.SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });

        }
    }
}
