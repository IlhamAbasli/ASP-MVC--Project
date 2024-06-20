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
    public class StatisticRepository : IStatisticRepository
    {
        private readonly AppDbContext _context;
        public StatisticRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Statistic statistic)
        {
            await _context.Statistics.AddAsync(statistic);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Statistic statistic)
        {
            _context.Statistics.Remove(statistic);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Statistic statistic)
        {
            var existStat = await GetById(id);
            existStat.Title = statistic.Title;
            existStat.Count = statistic.Count;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Statistic>> GetAll()
        {
            return await _context.Statistics.ToListAsync();
        }

        public async Task<Statistic> GetById(int id)
        {
            return await _context.Statistics.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
