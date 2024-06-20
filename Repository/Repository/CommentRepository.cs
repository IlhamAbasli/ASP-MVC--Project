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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _context.Comments.Include(m=>m.User).Include(m=>m.Product).ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.Where(m=>m.Id == id).Include(m=>m.User).Include(m=>m.Product).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetCommentByProduct(int id)
        {
            return await _context.Comments.Where(m => m.ProductId == id).Include(m=>m.User).ToListAsync();
        }
    }
}
