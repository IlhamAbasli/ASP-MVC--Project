using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAll();
        Task<Feature> GetById(int id);  
        Task Create(Feature feature);
        Task Edit(int id,Feature feature);
        Task Delete(Feature feature);
    }
}
