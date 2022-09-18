using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
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
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        public OrderStatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(OrderStatusDto status, CancellationToken cancellationToken)
        {
            var newStatus = new OrderStatus()
            {
                Name = status.Name,
                CreationDate = status.CreationDate,
            };
            await _context.OrderStatuses.AddAsync(newStatus, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newStatus.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var status = await _context.OrderStatuses.SingleAsync(x => x.Id == id, cancellationToken);
                _context.OrderStatuses.Remove(status);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("امکان حذف به دلیل استفاده شناسه وجود ندارد", ex.InnerException);
            }

        }

        public async Task Update(OrderStatusDto status, CancellationToken cancellationToken)
        {
            var status1 = await _context.OrderStatuses.SingleAsync(x => x.Id == status.Id, cancellationToken);
            status1.Name = status.Name;
            status1.CreationDate = status.CreationDate;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderStatusDto> Get(int id, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var statusDto = new OrderStatusDto()
            {
                Id = id,
                Name = status.Name,
                CreationDate = status.CreationDate,
                ShamsiCreationDate = status.CreationDate.ToShamsi(),
            };
            return statusDto;
        }

        public async Task<OrderStatusDto> Get(string name, CancellationToken cancellationToken)
        {
            var status = await _context.OrderStatuses
                .Where(x => x.Name == name).SingleOrDefaultAsync(cancellationToken);
            if (status == null)
                return null;
            var statusDto = new OrderStatusDto()
            {
                Id = status.Id,
                Name = status.Name,
                CreationDate = status.CreationDate,
                ShamsiCreationDate = status.CreationDate.ToShamsi(),
            };
            return statusDto;
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _context.OrderStatuses
                .Select(x => new OrderStatusDto()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
            return statuses;
        }
    }
}
