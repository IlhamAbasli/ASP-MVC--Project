using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<List<Statistic>> GetAll();
        Task<Statistic> GetById(int id);
        Task Create(Statistic statistic);
        Task Edit(int id, Statistic statistic);
        Task Delete(Statistic statistic);
    }
}
