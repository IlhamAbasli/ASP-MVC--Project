using Domain.Models;
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
    }
}
