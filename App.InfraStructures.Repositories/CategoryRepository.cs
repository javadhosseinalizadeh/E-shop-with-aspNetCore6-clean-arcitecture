using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> Add(CategoryDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.Category category = new()
            {
                Title = dto.Title,
            };
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            if (category.Id != 0)
            {
                _logger.LogInformation("دسته بندی جدید با موفقیت {عملیات} شد", "ایجاد");
            }
            else
            {
                _logger.LogWarning("{عملیات} دسته بندی جدید با خطا مواجه شد", "ایجاد");
            }
            return category.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(p => p.Id == id).SingleAsync(cancellationToken);

            _context.Remove(category!);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("دسته بندی جدید با موفقیت حذف شد", "عملیات");
        }

        public async Task<CategoryDto>? Get(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Id == id).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
            }).SingleOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"دسته بندی با آی دی {id} فراخوانی شد", "id");
            return category;
        }

        public async Task<CategoryDto>? Get(string title, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Title == title).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
            }).SingleOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"دسته بندی با آی دی {title} فراخوانی شد", "id");
            return category;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Categories.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
            }).ToListAsync(cancellationToken);
        }

        public Task<List<CategoryDto>> GetAllWithServices(CancellationToken cancellationToken)
        {
            var categories = _context.Categories.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                Services = c.Services.Select(s => new ServiceDto()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Price = s.Price,
                    ShortDescription = s.ShortDescription,
                    CategoryId = s.CategoryId,
                }).ToList()
            })
                .ToListAsync(cancellationToken);
            return categories;
        }

        public async Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Id == dto.Id).SingleOrDefaultAsync(cancellationToken);
            category.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("دسته بندی جدید با موفقیت {به روز رسانی} شد", "عملیات");
        }

    }
}
