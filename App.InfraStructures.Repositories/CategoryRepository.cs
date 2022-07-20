using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
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
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(CategoryDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.Category category = new()
            {
                Title = dto.Title,
            };
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(p => p.Id == id).SingleAsync(cancellationToken);

            _context.Remove(category!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CategoryDto>? Get(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Id == id).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
            }).SingleOrDefaultAsync(cancellationToken);
            return category;
        }

        public async Task<CategoryDto>? Get(string title, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Title == title).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
            }).SingleOrDefaultAsync(cancellationToken);
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

        public async Task Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c=>c.Id==dto.Id).SingleOrDefaultAsync(cancellationToken);
            category.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
