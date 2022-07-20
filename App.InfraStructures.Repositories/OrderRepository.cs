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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(OrderDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.Order order = new()
            {
                StatusId = dto.StatusId,
                ServiceId = dto.ServiceId,
                ServiceBasePrice = dto.ServiceBasePrice,
                CustomerUserId = dto.CustomerUserId,
                FinalExpertUserId = dto.FinalExpertUserId,
                CreatedAt = dto.CreatedAt,
            };
            await _context.AddAsync(order,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var order = _context.Orders.Where(o => o.Id == id).SingleAsync(cancellationToken);
            _context.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<OrderDto>? Get(int id, CancellationToken cancellationToken)
        {
            var order = _context.Orders.Where(o => o.Id == id).Select(o => new OrderDto()
            {
                Id = o.Id,
                StatusId = o.StatusId,
                ServiceId = o.ServiceId,
                ServiceBasePrice = o.ServiceBasePrice,
                CustomerUserId = o.CustomerUserId,
                FinalExpertUserId = o.FinalExpertUserId,
                CreatedAt = o.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return order;
        }


        public async Task Update(OrderDto dto, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Where(o => o.Id == dto.Id).SingleAsync(cancellationToken);
            order.StatusId = dto.StatusId;
            order.ServiceId= dto.ServiceId;
            order.ServiceBasePrice = dto.ServiceBasePrice;
            order.CustomerUserId = dto.CustomerUserId;
            order.FinalExpertUserId= dto.FinalExpertUserId;
            order.CreatedAt=dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
