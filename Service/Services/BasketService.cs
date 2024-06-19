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
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task Create(Basket basket)
        {
            await _basketRepository.Create(basket);
        }

        public async Task DecreaseExistProductCount(string name, string userId)
        {
            await _basketRepository.DecreaseExistProductCount(name, userId);
        }

        public async Task<bool> ExistProduct(string name, string userId)
        {
            return await _basketRepository.ExistProduct(name,userId);
        }

        public async Task<List<Basket>> GetAll()
        {
            return await _basketRepository.GetAll();
        }

        public async Task<List<Basket>> GetBasketByUser(string id)
        {
            return await _basketRepository.GetBasketByUser(id);
        }

        public async Task<Basket> GetBasketProductById(int id)
        {
            return await _basketRepository.GetBasketProductById(id);
        }

        public async Task<int> GetBasketProductCount(string id)
        {
            return await _basketRepository.GetBasketProductCount(id);
        }

        public async Task IncreaseExistProductCount(string name, string userId)
        {
            await _basketRepository.IncreaseExistProductCount(name,userId);
        }

        public async Task Remove(Basket basket)
        {
            await _basketRepository.Remove(basket);   
        }
    }
}
