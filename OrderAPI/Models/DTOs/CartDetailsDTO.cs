﻿namespace OrderAPI.Models.DTOs
{
    public class CartDetailsDTO
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public int Count { get; set; }
    }
}
