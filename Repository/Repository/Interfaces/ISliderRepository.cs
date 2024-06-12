using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface ISliderRepository
    {
        Task<List<Slider>> GetAll();
        Task<Slider> GetById(int id);
        Task Create (Slider slider);
        Task Delete(int id);    
        Task Edit(int id,Slider slider);
    }
}
