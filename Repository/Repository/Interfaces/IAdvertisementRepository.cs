using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> GetById(int id);
        Task Create(Advertisement advertisement);
        Task Delete(Advertisement advertisement);
        Task Edit(int id,Advertisement advertisement);
    }
}
