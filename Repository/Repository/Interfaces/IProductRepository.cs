using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product category);
        Task Edit(int id, Product category);
        Task Delete(Product product);
        Task<List<Quality>> GetQualities();
        Task DeleteImage(ProductImages image);
        Task ChangeMainImage(Product product, int id);
        Task<List<Product>> GetAllPaginatedDatas(int page,int take = 9);
        Task<List<Product>> GetAllSearchedPaginatedDatas(int page, string searchText, int take = 9);
        Task<List<Product>> GetAllPriceFilteredPaginatedDatas(int page, int price, int take = 9);
        Task<List<Product>> GetCategoryFilteredPaginatedDatas(int page, int categoryId, int take = 9);
        Task<List<Product>> GetSortedPaginatedDatas(int page, string sortType, int take = 9);
        Task<int> GetSearchedCount(string searchText);
        Task<int> GetPriceFilteredCount(int price);
        Task<int> GetCategoryFilteredCount(int categoryId);
        Task<int> GetCount();
        Task<List<Product>> GetBestSellerProducts();

        Task BuyProducts(List<Basket> basket);

        Task<List<Product>> GetVegetables();
    }
}
