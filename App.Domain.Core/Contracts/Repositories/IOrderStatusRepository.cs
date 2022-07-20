using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IOrderStatusRepository
    {

        #region "Queries"

        Task<OrderStatusDto>? Get(int id,CancellationToken cancellationToken);
        Task<OrderStatusDto>? Get(string title,CancellationToken cancellationToken);
        Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task Add(OrderStatusDto dto, CancellationToken cancellationToken);
        Task Update(OrderStatusDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
