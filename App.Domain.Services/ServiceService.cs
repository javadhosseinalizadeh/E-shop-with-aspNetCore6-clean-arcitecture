using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceFileRepository _serviceFileRepository;
        private readonly IConfiguration _configuration;


        public ServiceService(IServiceRepository serviceRepository, IServiceFileRepository serviceFileRepository, IConfiguration configuration)
        {
            _serviceRepository = serviceRepository;
            _serviceFileRepository = serviceFileRepository;
            _configuration = configuration;
        }

        public async Task<int> Add(ServiceDto serviceDTO, CancellationToken cancellationToken)
        {
            var result = await _serviceRepository.Add(serviceDTO, cancellationToken);
            return result;
        }

        public async Task<bool> AddServiceFiles(int ServiceId, List<int> fileIds, CancellationToken cancellationToken)
        {
            foreach (var fileId in fileIds)
            {
                ServiceFileDto serviceFile = new()
                {
                    FileId = fileId,
                    ServiceId = ServiceId,
                };
                var result = await _serviceFileRepository.Add(serviceFile, cancellationToken);
            }
            return true;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceRepository.Delete(id, cancellationToken);
        }

        public async Task DeleteServiceFile(int id, CancellationToken cancellationToken)
        {
            await _serviceRepository.DeleteServiceFile(id, cancellationToken);
        }

        public async Task EnsureServiceIsNotExist(string title, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.Get(title, cancellationToken);
            if (!(service == null))
                throw new Exception("خدمت مورد نظر قبلا ایجاد شده است");
        }

        public async Task<ServiceDto> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _serviceRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<ServiceDto>> GetAll(int id, CancellationToken cancellationToken)
        {
            var servcies = await _serviceRepository.GetAll(id, cancellationToken);
            return servcies;
        }

        public async Task<List<AppFileDto>> GetAllFiles(int ServiceId, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetSection("DownloadPath").Value;
            var paths = await _serviceRepository.GetAllFiles(ServiceId, cancellationToken);
            foreach (var path in paths)
            {
                path.Path = rootPath + "/" + path.Path;
            }
            return paths;
        }

        public async Task Update(ServiceDto serviceDTO, CancellationToken cancellationToken)
        {
            await _serviceRepository.Update(serviceDTO, cancellationToken);
        }
    }
}
