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

        Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken);
        Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken);
        Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken);


        #endregion



        #region "Commands"

        Task<int> Add(ServiceCommentDto dto , CancellationToken cancellationToken);
        Task Update(ServiceCommentDto dto , CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
