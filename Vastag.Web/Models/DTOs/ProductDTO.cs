using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vastag.Web.Models.DTOs
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Count = 1;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

        [Range(1,100)]
        public int Count { get; set; }

        public static implicit operator List<object>(ProductDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
