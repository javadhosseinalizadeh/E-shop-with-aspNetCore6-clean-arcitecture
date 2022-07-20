using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IServiceFileRepository
    {

        #region "Queries"

        Task<ServiceFileDto>? Get(int id, CancellationToken cancellationToken);
        Task<ServiceFileDto>? Get(string name, CancellationToken cancellationToken);
        Task<List<ServiceFileDto>> GetAll(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task Add(ServiceFileDto dto , CancellationToken cancellationToken);
        Task Update(ServiceFileDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
