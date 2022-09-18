using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface IServiceAppService
    {
        Task<List<ServiceDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<int> Add(ServiceDto serviceDTO, List<IFormFile> files, CancellationToken cancellationToken);
        Task<ServiceDto> Get(int id, CancellationToken cancellationToken);
        Task Update(ServiceDto serviceDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteServiceFile(int id, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);
        Task AddServiceFile(int id, List<IFormFile> files, CancellationToken cancellationToken);
    }
}
