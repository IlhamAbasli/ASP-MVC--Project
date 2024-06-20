using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();
        Task Create(Comment comment);
        Task<List<Comment>> GetCommentByProduct(int id);
    }
}
