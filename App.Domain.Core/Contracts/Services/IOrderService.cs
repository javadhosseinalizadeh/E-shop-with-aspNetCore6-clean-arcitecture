using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface IOrderService
    {
        Task<int> Add(OrderDto order, CancellationToken cancellationToken);
        Task<bool> AddOrderFiles(int OrderId, List<int> fileIds, CancellationToken cancellationToken);
        Task Update(OrderDto order, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task DeleteOrderFile(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<OrderDto> Get(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAllExpertOrders(AppUserDto expert, string query, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int orderId, CancellationToken cancellationToken);
    }
}
