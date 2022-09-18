using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface ICommentAppService
    {
        Task<int> Add(ServiceCommentDto comment, CancellationToken cancellationToken);
        Task<int> CreateOrderComment(int orderId, int serviceId, string title, string description, CancellationToken cancellationToken);
        Task Update(ServiceCommentDto comment, CancellationToken cancellationToken);
        Task ChangeCommentStatus(int commentId, bool status, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken);
        Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken);
        Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken);
        Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
