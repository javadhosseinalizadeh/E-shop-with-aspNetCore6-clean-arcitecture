using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IServiceCommentRepository
    {

        #region "Queries"

        Task<ServiceCommentDto>? Get(int id, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAll(CancellationToken cancellationToken);


        #endregion



        #region "Commands"

        Task Add(ServiceCommentDto dto , CancellationToken cancellationToken);
        Task Update(ServiceCommentDto dto , CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
