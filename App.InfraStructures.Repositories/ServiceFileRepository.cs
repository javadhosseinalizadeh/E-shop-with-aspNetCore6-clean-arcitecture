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
    public class ServiceFileRepository : IServiceFileRepository
    {
        private readonly AppDbContext _context;

        public ServiceFileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(ServiceFileDto serviceFile, CancellationToken cancellationToken)
        {
            ServiceFile file = new()
            {
                FileId = serviceFile.FileId,
                ServiceId = serviceFile.ServiceId,
            };
            await _context.ServiceFiles.AddAsync(file, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return file.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _context.ServiceFiles.SingleAsync(x => x.Id == id, cancellationToken);
            _context.ServiceFiles.Remove(serviceFile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ServiceFile> Get(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _context.ServiceFiles
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            return serviceFile;
        }

        public async Task<List<ServiceFile>> GetAll(CancellationToken cancellationToken)
        {
            var serviceFile = await _context.ServiceFiles.ToListAsync(cancellationToken);
            return serviceFile;
        }
    }
}
