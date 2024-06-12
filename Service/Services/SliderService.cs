using Domain.Models;
using Repository.Repository.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository) 
        {
            _sliderRepository = sliderRepository;
        }
        public async Task Create(Slider slider)
        {
            await _sliderRepository.Create(slider);
        }

        public async Task Delete(Slider slider)
        {
            await _sliderRepository.Delete(slider);
        }

        public async Task Edit(int id, Slider slider)
        {
            await _sliderRepository.Edit(id, slider);
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _sliderRepository.GetAll();
        }

        public async Task<Slider> GetById(int id)
        {
            return await _sliderRepository.GetById(id);
        }
    }
}
