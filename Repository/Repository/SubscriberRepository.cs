using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly AppDbContext _context;
        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Subscriber subscriber)
        {
            await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subscriber subscriber)
        {
            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subscriber>> GetAll()
        {
            return await _context.Subscribers.ToListAsync();
        }

        public async Task<Subscriber> GetById(int id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
