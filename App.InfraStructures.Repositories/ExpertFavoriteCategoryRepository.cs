using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
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
        public async Task<int> Add(ExpertFavoriteCategory expertCategory, CancellationToken cancellationToken)
        {
            await _context.ExpertFavoriteCategories.AddAsync(expertCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return expertCategory.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var fav = await _context.ExpertFavoriteCategories.Where(f => f.Id == id).SingleOrDefaultAsync(cancellationToken);
            _context.ExpertFavoriteCategories.Remove(fav!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ExpertFavoriteCategory>? Get(int id, CancellationToken cancellationToken)
        {
            var expertCategory = await _context.ExpertFavoriteCategories
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return expertCategory;
        }

        public async Task<List<ExpertFavoriteCategory>> GetAll(CancellationToken cancellationToken)
        {
            var expertCategorys = await _context.ExpertFavoriteCategories.ToListAsync(cancellationToken);
            return expertCategorys;
        }
        public async Task Update(ExpertFavoriteCategory dto, CancellationToken cancellationToken)
        {
            var fav = await _context.ExpertFavoriteCategories.Where(f=>f.Id ==dto.Id).SingleAsync(cancellationToken);
            fav.ExpertUserId = dto.ExpertUserId;
            fav.CategoryId = dto.CategoryId;
            fav.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
