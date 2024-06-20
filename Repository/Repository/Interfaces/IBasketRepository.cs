﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IBasketRepository
    {
        Task<List<Basket>> GetAll();
        Task Create(Basket basket);
        Task<List<Basket>> GetBasketByUser(string id);
        Task IncreaseExistProductCount(string name,string userId,int count = 1);
        Task DecreaseExistProductCount(string name, string userId);
        Task<bool> ExistProduct(string name,string userId);
        Task<int> GetBasketProductCount(string id);
        Task<Basket> GetBasketProductById(int id);
        Task Remove(Basket basket); 
    }
}
