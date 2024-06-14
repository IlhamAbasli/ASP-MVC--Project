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
    public class BannerRepository : IBannerRepository
    {
        private readonly AppDbContext _context;
        public BannerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Banner banner)
        {
            await _context.Banners.AddAsync(banner);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Banner banner)
        {
            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Banner banner)
        {
            var existBanner = await GetById(id);

            existBanner.Title = banner.Title;
            existBanner.Description = banner.Description;
            existBanner.Image = banner.Image;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Banner>> GetAll()
        {
            return await _context.Banners.ToListAsync();
        }

        public async Task<Banner> GetById(int id)
        {
            return await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
