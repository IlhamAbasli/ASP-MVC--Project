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
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly AppDbContext _context;
        public AdvertisementRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Advertisement advertisement)
        {
            await _context.Advertisements.AddAsync(advertisement);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Advertisement advertisement)
        {
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Advertisement advertisement)
        {
            var existAd = await GetById(id);
            existAd.Title = advertisement.Title;
            existAd.Description = advertisement.Description;
            existAd.AdImage = advertisement.AdImage;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Advertisement>> GetAll()
        {
            return await _context.Advertisements.ToListAsync();
        }

        public async Task<Advertisement> GetById(int id)
        {
            return await _context.Advertisements.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
