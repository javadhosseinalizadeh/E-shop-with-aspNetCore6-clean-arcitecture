using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface ICommentService
    {
        Task<int> Add(ServiceCommentDto comment, CancellationToken cancellationToken);
        Task Update(ServiceCommentDto comment, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken);
        Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken);
        Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken);
    }
}
