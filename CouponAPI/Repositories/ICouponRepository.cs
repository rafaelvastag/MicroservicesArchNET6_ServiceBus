using CouponAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponAPI.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string code);
    }
}
