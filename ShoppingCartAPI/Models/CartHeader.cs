﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class CartHeader
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
