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
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceDto> Get(int id, CancellationToken cancellationToken)
        {
            var service = await _context.Services
        .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var serviceDto = new ServiceDto()
            {
                Id = id,
                CategoryId = service.CategoryId,
                ShortDescription = service.ShortDescription,
                Price = service.Price,
                Title = service.Title,
            };
            return serviceDto;
        }

        public async Task<ServiceDto> Get(string name, CancellationToken cancellationToken)
        {
            var service = await _context.Services
        .Where(x => x.Title == name).SingleOrDefaultAsync(cancellationToken);
            if (service == null)
                return null;
            var serviceDto = new ServiceDto()
            {
                Id = service.Id,
                CategoryId = service.CategoryId,
                ShortDescription = service.ShortDescription,
                Price = service.Price,
                Title = service.Title,
            };
            return serviceDto;
        }

        public async Task<List<ServiceDto>> GetAll(int id, CancellationToken cancellationToken)
        {
            IQueryable<Service> query = _context.Services;
            if (id != 0)
            {
                query = query.Where(x => x.CategoryId == id);
            }
            var services = await query
        .Select(x => new ServiceDto()
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            ShortDescription = x.ShortDescription,
            Price = x.Price,
            Title = x.Title,
        })
        .ToListAsync(cancellationToken);
            return services;
        }

        public async Task<List<AppFileDto>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var files = await _context.ServiceFiles.Where(x => x.ServiceId == ServiceId).Select(x => x.File).Select(x => new AppFileDto()
            {
                Id = x.Id,
                Path = x.Path,
                CreationDate = x.CreationDate,
            }).ToListAsync(cancellationToken);
            return files;
        }
        public async Task<int> Add(ServiceDto serviceDTO, CancellationToken cancellationToken)
        {
            var service = new Service()
            {
                ShortDescription = serviceDTO.ShortDescription,
                CategoryId = serviceDTO.CategoryId,
                Price = serviceDTO.Price,
                Title = serviceDTO.Title,

            };
            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return service.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _context.Services.SingleAsync(x => x.Id == id, cancellationToken);
                _context.Services.Remove(tag);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("امکان حذف به دلیل استفاده شناسه وجود ندارد", ex.InnerException);
            }

        }

        public async Task DeleteServiceFile(int id, CancellationToken cancellationToken)
        {
            var serviceFile = await _context.Files.SingleAsync(x => x.Id == id, cancellationToken);
            _context.Remove(serviceFile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ServiceDto serviceDTO, CancellationToken cancellationToken)
        {
            var service = await _context.Services.SingleAsync(x => x.Id == serviceDTO.Id, cancellationToken);
            service.Title = serviceDTO.Title;
            service.ShortDescription = serviceDTO.ShortDescription;
            service.CategoryId = serviceDTO.CategoryId;
            service.Price = serviceDTO.Price;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
