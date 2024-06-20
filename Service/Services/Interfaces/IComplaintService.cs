using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IComplaintService
    {
        Task Create(ComplaintSuggest suggest);
        Task<List<ComplaintSuggest>> GetAll();
        Task Delete(ComplaintSuggest suggest);
        Task<ComplaintSuggest> GetById(int id); 
    }
}
