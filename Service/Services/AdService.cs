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
    public class AdService : IAdService
    {
        private readonly IAdvertisementRepository _adRepository;
        public AdService(IAdvertisementRepository adRepository)
        {
            _adRepository = adRepository;
        }
        public async Task Create(Advertisement advertisement)
        {
            await _adRepository.Create(advertisement);
        }

        public async Task Delete(Advertisement advertisement)
        {
            await _adRepository.Delete(advertisement);
        }

        public async Task Edit(int id, Advertisement advertisement)
        {
            await _adRepository.Edit(id, advertisement);
        }

        public async Task<List<Advertisement>> GetAll()
        {
            return await _adRepository.GetAll();
        }

        public async Task<Advertisement> GetById(int id)
        {
            return await _adRepository.GetById(id);
        }
    }
}
