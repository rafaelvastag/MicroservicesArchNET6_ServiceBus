using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationContext _context;
        private IMapper _mapper;

        public ProductRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            List<ProductEntity> productList = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(productList);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            ProductEntity p = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDTO>(p);
        }

        public async Task<ProductDTO> CreateUpdateProduct(ProductDTO p)
        {
            ProductEntity product = _mapper.Map<ProductDTO, ProductEntity>(p);

            if (product.Id > 0)
            {
                _context.Products.Update(product);
            }
            else
            {
                _context.Products.Add(product);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductEntity, ProductDTO>(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                ProductEntity p = await _context.Products.FirstOrDefaultAsync(u => u.Id == id);

                if (null == p)
                {
                    return false;
                }
                _context.Products.Remove(p);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
