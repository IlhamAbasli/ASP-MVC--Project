using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Product category)
        {
            await _context.Products.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Product category)
        {
            var existProduct = await GetById(id);
            existProduct.Name = category.Name;
            existProduct.Description = category.Description;
            existProduct.ProductImages = category.ProductImages;
            existProduct.Count = category.Count;
            existProduct.Details = category.Details;
            existProduct.CategoryId = category.CategoryId;
            existProduct.RatingCount = category.RatingCount;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.Include(m=>m.Details)
                                          .Include(m=>m.Category)
                                          .Include(m=>m.ProductImages)
                                          .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Where(m=>m.Id == id)
                                          .Include(m => m.Details)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImages).FirstOrDefaultAsync();
        }
    }
}
