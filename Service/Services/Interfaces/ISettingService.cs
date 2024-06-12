using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISettingService
    {
        Task<List<Setting>> GetAll();
        Task<Setting> GetById(int id);
        Task Create(Setting setting);
        Task Edit(int id, Setting setting);
        Task Delete(int id);
    }
}
