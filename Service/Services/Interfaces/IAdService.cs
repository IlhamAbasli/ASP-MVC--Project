using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAdService
    {
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> GetById(int id);
        Task Create(Advertisement advertisement);
        Task Edit(int id, Advertisement advertisement);
        Task Delete(Advertisement advertisement);
    }
}
