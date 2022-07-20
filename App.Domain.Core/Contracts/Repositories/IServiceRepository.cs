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

        Task<ServiceDto>? Get(int id,CancellationToken cancellationToken);


        #endregion



        #region "Commands"

        Task Add(ServiceDto dto , CancellationToken cancellationToken);
        Task Update(ServiceDto dto, CancellationToken cancellationToken);
        Task Delete(int id , CancellationToken cancellationToken);

        #endregion
    }
}
