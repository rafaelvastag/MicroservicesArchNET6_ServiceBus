using System.Threading.Tasks;

namespace Vastag.Web.Services
{
    public interface ICouponService
    {

        Task<T> GetCouponDetails<T>(string couponCode, string token = null);
    }
}
