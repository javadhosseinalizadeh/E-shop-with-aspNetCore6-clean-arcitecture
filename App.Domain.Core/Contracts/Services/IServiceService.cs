using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<int> Add(ServiceDto serviceDTO, CancellationToken cancellationToken);
        Task<ServiceDto> Get(int id, CancellationToken cancellationToken);
        Task Update(ServiceDto serviceDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteServiceFile(int id, CancellationToken cancellationToken);
        Task<bool> AddServiceFiles(int ServiceId, List<int> fileIds, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);
        Task EnsureServiceIsNotExist(string title, CancellationToken cancellationToken);
    }
}
