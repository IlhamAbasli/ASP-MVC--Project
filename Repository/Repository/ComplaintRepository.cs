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
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly AppDbContext _context;
        public ComplaintRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ComplaintSuggest suggest)
        {
            await _context.ComplaintSuggests.AddAsync(suggest);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ComplaintSuggest suggest)
        {
            _context.ComplaintSuggests.Remove(suggest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ComplaintSuggest>> GetAll()
        {
            return await _context.ComplaintSuggests.ToListAsync();
        }

        public async Task<ComplaintSuggest> GetById(int id)
        {
            return await _context.ComplaintSuggests.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
