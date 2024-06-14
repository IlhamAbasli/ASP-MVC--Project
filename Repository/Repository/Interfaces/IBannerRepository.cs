using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IBannerRepository
    {
        Task<List<Banner>> GetAll();
        Task<Banner> GetById(int id);
        Task Create(Banner banner);
        Task Edit(int id,Banner banner);
        Task Delete(Banner banner);
    }
}
