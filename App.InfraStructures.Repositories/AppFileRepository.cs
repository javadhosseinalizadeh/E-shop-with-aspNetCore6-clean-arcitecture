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
    public class AppFileRepository : IAppFileRepository
    {
        private readonly AppDbContext _context;

        public AppFileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(AppFileDto file, CancellationToken cancellationToken)
        {
            var newFile = new AppFile()
            {
                CreationDate = file.CreationDate,

                Path = file.Path
            };
            await _context.Files.AddAsync(newFile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newFile.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var file = await _context.Files.SingleAsync(x => x.Id == id, cancellationToken);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<AppFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            var file = await _context.Files
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var fileDto = new AppFileDto()
            {
                Id = id,
                CreationDate = file.CreationDate,
                Path = file.Path
            };
            return fileDto;
        }

        public async Task<AppFileDto>? Get(string path, CancellationToken cancellationToken)
        {
            var file = await _context.Files
               .Where(x => x.Path.ToLower() == path.ToLower()).SingleAsync(cancellationToken);
            var fileDto = new AppFileDto()
            {
                Id = file.Id,
                CreationDate = file.CreationDate,
                Path = file.Path
            };
            return fileDto;
        }

        public async Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken)
        {
            var query = _context.OrderFiles.Include(x => x.File).Include(x => x.Order).ToList();
            var result = _context.Orders.Include(x => x.OrderFiles).SelectMany(x => x.OrderFiles).ToList();

            var files = await _context.Files
                .Select(x => new AppFileDto()
                {
                    Id = x.Id,
                    Path = x.Path,
                    CreationDate = x.CreationDate,
                })
                .ToListAsync(cancellationToken);
            return files;
        }

        public async Task Update(AppFileDto file, CancellationToken cancellationToken)
        {
            var file1 = await _context.Files.SingleAsync(x => x.Id == file.Id, cancellationToken);
            file1.Path = file.Path;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
