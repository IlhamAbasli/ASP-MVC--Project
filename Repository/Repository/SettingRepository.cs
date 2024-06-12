﻿using Domain.Models;
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
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Setting setting)
        {
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();  
        }

        public async Task Edit(int id,Setting setting)
        {
            var existData = await GetById(id);

            existData.Key = setting.Key;
            existData.Value = setting.Value;

            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<int,Dictionary<string, string>>> GetAll()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Id, m => new Dictionary<string, string> { {"Key",m.Key },{"Value",m.Value }});
        }

        public async Task<Setting> GetById(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
