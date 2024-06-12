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
    public class SliderInfoRepository : ISliderInfoRepository
    {
        private readonly AppDbContext _context;
        public SliderInfoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(SliderInfo sliderInfo)
        {
            await _context.SliderInfos.AddAsync(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SliderInfo sliderInfo)
        {
            _context.SliderInfos.Remove(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, SliderInfo sliderInfo)
        {
            var existInfo = await GetById(id);

            existInfo.Title = sliderInfo.Title;
            existInfo.Description = sliderInfo.Description;
            existInfo.BackgroundImage = sliderInfo.BackgroundImage;

            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderInfo>> GetAll()
        {
            return await _context.SliderInfos.ToListAsync();
        }

        public async Task<SliderInfo> GetById(int id)
        {
            return await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
