using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<List<SliderInfo>> GetAll();
        Task<SliderInfo> GetById(int id);
        Task Create(SliderInfo sliderInfo);
        Task Delete(SliderInfo sliderInfo);
        Task Edit(int id, SliderInfo sliderInfo);
    }
}
