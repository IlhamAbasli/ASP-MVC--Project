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
    public class SliderInfoService : ISliderInfoService
    {
        private readonly ISliderInfoRepository _sliderInfoRepository;

        public SliderInfoService(ISliderInfoRepository sliderInfoRepository)
        {
            _sliderInfoRepository = sliderInfoRepository;
        }
        public async Task Create(SliderInfo sliderInfo)
        {
            await _sliderInfoRepository.Create(sliderInfo);
        }

        public async Task Delete(SliderInfo sliderInfo)
        {
            await _sliderInfoRepository.Delete(sliderInfo);
        }

        public async Task Edit(int id, SliderInfo sliderInfo)
        {
            await _sliderInfoRepository.Edit(id, sliderInfo);
        }

        public async Task<List<SliderInfo>> GetAll()
        {
            return await _sliderInfoRepository.GetAll();
        }

        public async Task<SliderInfo> GetById(int id)
        {
            return await _sliderInfoRepository.GetById(id);
        }
    }
}
