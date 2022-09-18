using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IOrderRepository
    {

        #region "Queries"

        Task<List<OrderDto>> GetAll(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAllExpertOrders(AppUserDto expert, string query, CancellationToken cancellationToken);
        Task<OrderDto> Get(int id, CancellationToken cancellationToken);
        Task<List<AppFileDto>> GetAllFiles(int orderId, CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task<int> Add(OrderDto dto,CancellationToken cancellationToken);
        Task Update(OrderDto dto, CancellationToken cancellationToken);
        Task Delete(int id , CancellationToken cancellationToken);
        Task DeleteOrderFile(int id, CancellationToken cancellationToken);

        #endregion
    }
}
