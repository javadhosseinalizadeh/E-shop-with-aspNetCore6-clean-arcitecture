using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace App.InfraStructures.Repositories
{
    public class OrderFileRepository : IOrderFileRepository
    {
        private readonly AppDbContext _context;

        public OrderFileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(OrderFileDto dto, CancellationToken cancellationToken)
        {
            var newOrderFile = new OrderFile()
            {
                OrderId = dto.OrderId,
                FileId = dto.FileId,
            };
            await _context.OrderFiles.AddAsync(newOrderFile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return dto.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var orderFile = await _context.OrderFiles.SingleAsync(x => x.Id == id, cancellationToken);
            _context.OrderFiles.Remove(orderFile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderFile> Get(int id, CancellationToken cancellationToken)
        {
            var orderFile = await _context.OrderFiles
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return orderFile;
        }



        public async Task<List<OrderFile>> GetAll(CancellationToken cancellationToken)
        {
            var orderFiles = await _context.OrderFiles.ToListAsync(cancellationToken);
            return orderFiles;
        }


    }
}
