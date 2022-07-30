using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            await _categoryRepository.Delete(id,cancellationToken);
        }

        public async Task EnsureDoesNotExist(string name, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Get(name,cancellationToken);
            if (category != null)
                throw new Exception($"there is already a category with name = {name}");
        }

        public async Task EnsureExists(string name, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.Get(name, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Category with name : {name}!");
            }
        }

        public async Task EnsureExists(int id, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.Get(id, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Category with id : {id}!");
            }
        }

        public async Task<CategoryDto> Get(int id, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.Get(id, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Category with id : {id}!");
            }
            return record;
        }

        public async Task<CategoryDto> Get(string name, CancellationToken cancellationToken)
        {
            var record = await _categoryRepository.Get(name, cancellationToken);
            if (record == null)
            {
                throw new Exception($"No Category with name : {name}!");
            }
            return record;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll( cancellationToken);
        }

        public async Task Set(CategoryDto dto,CancellationToken cancellationToken)
        {
            await _categoryRepository.Add(dto, cancellationToken);
        }

        public async Task Update(CategoryDto dto,CancellationToken cancellationToken)
        {
            await _categoryRepository.Update(dto, cancellationToken);
        }
    }
}
