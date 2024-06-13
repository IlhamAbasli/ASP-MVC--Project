using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product category);
        Task Edit(int id, Product category);
        Task Delete(Product product);
        Task<List<Quality>> GetQualities();
        Task DeleteImage(ProductImages image);
        Task ChangeMainImage(Product product, int id);  
    }
}
