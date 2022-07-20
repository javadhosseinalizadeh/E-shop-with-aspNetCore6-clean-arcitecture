using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IOrderFileRepository
    {

        #region "Queries"

        Task<OrderFileDto>? Get(int id, CancellationToken cancellationToken);
        Task<OrderFileDto>? Get(string name, CancellationToken cancellationToken);
        Task<List<OrderFileDto>> GetAll(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task Add(OrderFileDto dto, CancellationToken cancellationToken);
        Task Update(OrderFileDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
