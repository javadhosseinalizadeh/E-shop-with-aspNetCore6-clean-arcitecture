using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _service;
        public CategoryAppService(ICategoryService categoryService)
        {
            _service = categoryService;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _service.EnsureExists(id, cancellationToken);
            await _service.Delete(id, cancellationToken);
        }

        public async Task<CategoryDto> Get(int id, CancellationToken cancellationToken)
        {
            return await _service.Get(id, cancellationToken);
        }

        public async Task<CategoryDto> Get(string name, CancellationToken cancellationToken)
        {
            return await _service.Get(name, cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAll(cancellationToken);
        }

        public async Task Set(CategoryDto dto, CancellationToken cancellationToken)
        {
            await _service.EnsureDoesNotExist(dto.Title, cancellationToken);
            await _service.Set(dto, cancellationToken);
        }

        public async Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            await _service.EnsureExists(dto.Id, cancellationToken);
            await _service.Update(dto, cancellationToken);
        }
    }
}
