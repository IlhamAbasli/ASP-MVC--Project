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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        public Task<bool> CategoryIsExist(string categoryName)
        {
            return _categoryRepository.CategoryIsExist(categoryName);
        }

        public async Task Create(Category category)
        {
            await _categoryRepository.Create(category);
        }

        public async Task Delete(Category category)
        {
           await _categoryRepository.Delete(category);
        }

        public async Task Edit(int id, Category category)
        {
            await _categoryRepository.Edit(id,category);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }
    }
}
