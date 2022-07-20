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
    public class ExpertFavoriteCategoryRepository : IExpertFavoriteCategoryRepository
    {
        private readonly AppDbContext _context;
        public ExpertFavoriteCategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(ExpertFavoriteCategoryDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.ExpertFavoriteCategory fav = new()
            {
                ExpertUserId = dto.ExpertUserId,
                CategoryId = dto.CategoryId,
                CreatedAt = dto.CreatedAt,
            };
            await _context.ExpertFavoriteCategories.AddAsync(fav,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var fav = await _context.ExpertFavoriteCategories.Where(f => f.Id == id).SingleOrDefaultAsync(cancellationToken);
            _context.Remove(fav!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ExpertFavoriteCategoryDto>? Get(int id, CancellationToken cancellationToken)
        {
            var fav = await _context.ExpertFavoriteCategories.Where(f => f.Id == id).Select(f => new ExpertFavoriteCategoryDto()
            {
                Id = f.Id,
                ExpertUserId = f.ExpertUserId,
                CategoryId = f.CategoryId,
                CreatedAt = f.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return fav;
        }

        public async Task Update(ExpertFavoriteCategoryDto dto, CancellationToken cancellationToken)
        {
            var fav = await _context.ExpertFavoriteCategories.Where(f=>f.Id ==dto.Id).SingleAsync(cancellationToken);
            fav.ExpertUserId = dto.ExpertUserId;
            fav.CategoryId = dto.CategoryId;
            fav.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
