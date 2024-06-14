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
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task Create(Feature feature)
        {
            await _featureRepository.Create(feature);
        }

        public async Task Delete(Feature feature)
        {
            await _featureRepository.Delete(feature);
        }

        public async Task Edit(int id, Feature feature)
        {
            await _featureRepository.Edit(id, feature);
        }

        public async Task<List<Feature>> GetAll()
        {
            return await _featureRepository.GetAll();
        }

        public async Task<Feature> GetById(int id)
        {
            return await _featureRepository.GetById(id);
        }
    }
}
