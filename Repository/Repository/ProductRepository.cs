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
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.Include(m=>m.Category)
                                          .Include(m=>m.ProductImages)
                                          .Include(m => m.Details)
                                          .ThenInclude(m => m.Qualities)
                                          .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Where(m=>m.Id == id)
                                          .Include(m => m.Details)
                                          .ThenInclude(m=>m.Qualities)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImages).FirstOrDefaultAsync();
        }

        public async Task<List<Quality>> GetQualities()
        {
            return await _context.Qualities.ToListAsync();
        }

        public async Task DeleteImage(ProductImages image)
        {
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeMainImage(Product product,int imageId)
        {
            var images = product.ProductImages.Where(m => m.IsMain == true);
            foreach(var image in images)
            {
                image.IsMain = false;
            }

            product.ProductImages.FirstOrDefault(m => m.Id == imageId).IsMain = true;
            await _context.SaveChangesAsync();

        }

        public async Task<List<Product>> GetAllPaginatedDatas(int page, int take = 9)
        {
            return await _context.Products.Include(m => m.ProductImages)
                                          .Include(m => m.Category)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> GetAllSearchedPaginatedDatas(int page, string searchText, int take = 9)
        {
            return await _context.Products.Where(m=> m.Name.Trim().ToLower().Contains(searchText.Trim().ToLower()))
                                          .Include(m => m.ProductImages)
                                          .Include(m => m.Category)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<int> GetSearchedCount(string searchText)
        {
            return await _context.Products.Where(m => m.Name.Trim().ToLower().Contains(searchText.Trim().ToLower())).CountAsync();
        }
    }
}
