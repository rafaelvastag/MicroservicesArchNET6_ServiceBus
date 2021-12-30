using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vastag.Web.Models.DTOs
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
    }
}
