using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface IOrderAppService
    {
        Task<int> AddNewOrder(OrderDto order, List<IFormFile> files, CancellationToken cancellationToken);
        Task Update(OrderDto order, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task ChangeOrderStatus(int orderId, CancellationToken cancellationToken);
        Task DeleteOrderFile(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<OrderDto> Get(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAllExpertOrders(string query, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int orderId, CancellationToken cancellationToken);
        Task AcceptOrderSuggest(int suggestId, CancellationToken cancellationToken);

    }
}
