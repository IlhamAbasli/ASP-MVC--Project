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
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task IncreaseExistProductCount(string name,string userId)
        {
            var existProduct = _context.Baskets.FirstOrDefault(b => b.ProductName == name && b.UserId == userId); 
            existProduct.ProductCount++;
            await _context.SaveChangesAsync();

        }

        public async Task DecreaseExistProductCount(string name, string userId)
        {
            var existProduct = _context.Baskets.FirstOrDefault(b => b.ProductName == name && b.UserId == userId);
            existProduct.ProductCount--;
            await _context.SaveChangesAsync();

        }

        public async Task Create(Basket basket)
        {
            await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Basket>> GetAll()
        {
            return await _context.Baskets.ToListAsync();
        }

        public async Task<List<Basket>> GetBasketByUser(string id)
        {
            return await _context.Baskets.Where(m=>m.UserId == id).ToListAsync();
        }

        public async Task<int> GetBasketProductCount(string id)
        {
            var products = await GetBasketByUser(id);
            return products.Count;
        }

        public async Task<bool> ExistProduct(string name, string userId)
        {
            return await _context.Baskets.AnyAsync(m => m.ProductName == name && m.UserId == userId);
        }

        public async Task<Basket> GetBasketProductById(int id)
        {
            return await _context.Baskets.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Remove(Basket basket)
        {
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }
    }
}
