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
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        public OrderStatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.OrderStatus status = new()
            {
                Id = dto.Id,
                Title = dto.Title,
            };
            await _context.AddAsync(status,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.Where(s => s.Id == id).SingleAsync(cancellationToken);
            _context.Remove(status);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderStatusDto>? Get(int id, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.Where(s=>s.Id==id).Select(s=>new OrderStatusDto()
            {
                Id=s.Id,
                Title = s.Title,

            }).SingleOrDefaultAsync(cancellationToken);
            return status;
        }

        public async Task<OrderStatusDto>? Get(string title, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.Where(s => s.Title == title).Select(s => new OrderStatusDto()
            {
                Id = s.Id,
                Title = s.Title,

            }).SingleOrDefaultAsync(cancellationToken);
            return status;
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.OrderStatuses.Select(s => new OrderStatusDto()
            {
                Id = s.Id,
                Title = s.Title,
            }).ToListAsync(cancellationToken);
        }

        public async Task Update(OrderStatusDto dto, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses.Where(s=>s.Id==dto.Id).SingleAsync(cancellationToken);
            status.Title = dto.Title;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
