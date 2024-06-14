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
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public async Task Create(Banner banner)
        {
            await _bannerRepository.Create(banner);
        }

        public async Task Delete(Banner banner)
        {
            await _bannerRepository.Delete(banner);
        }

        public async Task Edit(int id, Banner banner)
        {
            await _bannerRepository.Edit(id, banner);
        }

        public async Task<List<Banner>> GetAll()
        {
            return await _bannerRepository.GetAll();
        }

        public async Task<Banner> GetById(int id)
        {
            return await _bannerRepository.GetById(id);
        }
    }
}
