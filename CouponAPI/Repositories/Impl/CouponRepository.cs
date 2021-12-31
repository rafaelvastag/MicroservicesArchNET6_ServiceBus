using AutoMapper;
using CouponAPI.Context;
using CouponAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponAPI.Repositories.Impl
{
    public class CouponRepository : ICouponRepository
    {

        private readonly ApplicationDbContext _context;
        protected IMapper _mapper;

        public CouponRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string code)
        {
            var couponFromDb = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
            return _mapper.Map<CouponDTO>(couponFromDb);
        }
    }
}
