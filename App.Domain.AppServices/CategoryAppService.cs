using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _service;
        private readonly IDistributedCache _cache;

        public CategoryAppService(ICategoryService categoryService, IDistributedCache cache)
        {
            _service = categoryService;
            _cache = cache;
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
            List<CategoryDto> result = new List<CategoryDto>();
            if (_cache.Get("result") != null)
            {
                var _byte = _cache.Get("result");
                var _string = Encoding.UTF8.GetString(_byte);
                result = JsonSerializer.Deserialize<List<CategoryDto>>(_string);
            }
            else
            {
                result = await _service.GetAll(cancellationToken);
                var _string = JsonSerializer.Serialize(result);
                var _byte = Encoding.UTF8.GetBytes(_string);
                var option = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(800),
                };
                _cache.Set("result", _byte, option);
            }
            return result;
        }

        public async Task<List<CategoryDto>> GetAllWithServices(CancellationToken cancellationToken)
        {
            var categories = new List<CategoryDto>();

            if (_cache.Get("Categories") != null)
            {
                var categoryBytes = _cache.Get("Categories");
                var categoryString = Encoding.UTF8.GetString(categoryBytes);
                categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryString);
            }
            else
            {
                categories = await _service.GetAllWithServices(cancellationToken);
                var categoryString = JsonSerializer.Serialize(categories);
                var categoryBytes = Encoding.UTF8.GetBytes(categoryString);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20),
                };
                _cache.Set("Categories", categoryBytes, options);
            }
            return categories;
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
