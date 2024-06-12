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
    public class SettingRepository : ISettingRepository
    {
        private readonly AppDbContext _context;
        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Setting setting)
        {
            await _context.Settings.AddAsync(setting);
        }

        public async Task Delete(int id)
        {
            var existData = await GetById(id);
            _context.Settings.Remove(existData);
            await _context.SaveChangesAsync();  
        }

        public async Task Edit(int id,Setting setting)
        {
            var existData = await GetById(id);
            if (existData is null) return;

            existData.Key = setting.Key;
            existData.Value = setting.Value;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Setting>> GetAll()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetById(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
