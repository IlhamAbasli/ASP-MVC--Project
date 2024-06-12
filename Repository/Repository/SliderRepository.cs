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
    public class SliderRepository : ISliderRepository
    {
        private readonly AppDbContext _context;
        public SliderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existData = await GetById(id);
            _context.Sliders.Remove(existData);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Slider slider)
        {
            var existData = await GetById(id);
            existData.SliderText = slider.SliderText;
            existData.SliderImage = slider.SliderImage;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetById(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
