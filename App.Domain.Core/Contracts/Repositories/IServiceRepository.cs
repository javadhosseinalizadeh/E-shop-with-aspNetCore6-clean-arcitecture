using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IServiceRepository
    {

        #region "Queries"

        Task<List<ServiceDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<ServiceDto> Get(int id, CancellationToken cancellationToken);
        Task<ServiceDto> Get(string name, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int ServiceId, CancellationToken cancellationToken);


        #endregion



        #region "Commands"

        Task<int> Add(ServiceDto serviceDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteServiceFile(int id, CancellationToken cancellationToken);
        Task Update(ServiceDto serviceDTO, CancellationToken cancellationToken);

        #endregion
    }
}
