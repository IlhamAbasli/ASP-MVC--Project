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
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        public StatisticService(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }
        public async Task Create(Statistic statistic)
        {
            await _statisticRepository.Create(statistic);
        }

        public async Task Delete(Statistic statistic)
        {
            await _statisticRepository.Delete(statistic);
        }

        public async Task Edit(int id, Statistic statistic)
        {
            await _statisticRepository.Edit(id,statistic);
        }

        public async Task<List<Statistic>> GetAll()
        {
            return await _statisticRepository.GetAll();
        }

        public async Task<Statistic> GetById(int id)
        {
            return await _statisticRepository.GetById(id);
        }
    }
}
