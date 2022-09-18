using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
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

        Task<ServiceFile> Get(int id, CancellationToken cancellationToken);
        Task<List<ServiceFile>> GetAll(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task<int> Add(ServiceFileDto dto , CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
