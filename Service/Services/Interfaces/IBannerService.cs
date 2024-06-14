using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAll();
        Task<Banner> GetById(int id);
        Task Delete(Banner banner);
        Task Create(Banner banner);
        Task Edit(int id, Banner banner);
    }
}
