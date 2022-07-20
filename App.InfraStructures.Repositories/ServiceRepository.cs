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
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.Service service = new()
            {
                Id = dto.Id,
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Price = dto.Price,
            };
            await _context.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var service = await _context.Services.Where(s => s.Id == id).SingleAsync(cancellationToken);
            _context.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ServiceDto>? Get(int id, CancellationToken cancellationToken)
        {
            var service = await _context.Services.Where(s => s.Id == id).Select(s => new ServiceDto()
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                Title = s.Title,
                ShortDescription = s.ShortDescription,
                Price = s.Price,
            }).SingleOrDefaultAsync(cancellationToken);
            return service;
        }

        public async Task Update(ServiceDto dto, CancellationToken cancellationToken)
        {
            var service = await _context.Services.Where(s => s.Id == dto.Id).SingleAsync(cancellationToken);
            service.CategoryId = dto.CategoryId;
            service.Title = dto.Title;
            service.ShortDescription = dto.ShortDescription;
            service.Price = dto.Price;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
