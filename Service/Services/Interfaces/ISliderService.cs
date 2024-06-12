using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAll();
        Task<Slider> GetById(int id);
        Task Create (Slider slider);
        Task Edit (int id,Slider slider);
        Task Delete (Slider slider);
        
    }
}
