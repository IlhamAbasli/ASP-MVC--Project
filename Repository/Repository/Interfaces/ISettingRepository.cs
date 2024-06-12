using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface ISettingRepository
    {
        Task<Dictionary<int, Dictionary<string,string>>> GetAll();
        Task<Setting> GetById(int id);
        Task Create(Setting setting);
        Task Edit (int id,Setting setting);
        Task Delete(Setting setting);
    }
}
