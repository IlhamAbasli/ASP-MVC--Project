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
    public class FeatureRepository : IFeatureRepository
    {
        private readonly AppDbContext _context;
        public FeatureRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Feature feature)
        {
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Feature feature)
        {
            var existFeature = await GetById(id);
            existFeature.FeatureName = feature.FeatureName;
            existFeature.FeatureDesc = feature.FeatureDesc;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Feature>> GetAll()
        {
            return await _context.Features.ToListAsync();
        }

        public async Task<Feature> GetById(int id)
        {
            return await _context.Features.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
