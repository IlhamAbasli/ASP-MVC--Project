using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Create (Category category);
        Task Edit (int id,Category category);
        Task Delete(Category category);
        Task<bool> CategoryIsExist(string name);
    }
}
