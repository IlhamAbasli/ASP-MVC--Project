﻿using Domain.Models;
using Repository.Repository.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ChangeMainImage(Product product, int id)
        {
            await _productRepository.ChangeMainImage(product, id);
        }

        public async Task Create(Product category)
        {
            await _productRepository.Create(category);
        }

        public async Task Delete(Product product)
        {
            await _productRepository.Delete(product);
        }

        public async Task DeleteImage(ProductImages image)
        {
            await _productRepository.DeleteImage(image);
        }

        public async Task Edit(int id, Product category)
        {
            await _productRepository.Edit(id, category);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<List<Quality>> GetQualities()
        {
            return await _productRepository.GetQualities();
        }
    }
}
