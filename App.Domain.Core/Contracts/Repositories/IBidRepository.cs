using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IBidRepository
    {

        #region "Queries"

        Task<BidDto>? Get(int id,CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task<int> Add(BidDto dto, CancellationToken cancellationToken);
        Task Update(BidDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
