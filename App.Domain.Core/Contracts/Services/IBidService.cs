using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface IBidService
    {
        Task<int> Add(BidDto suggest, CancellationToken cancellationToken);
        Task Update(BidDto suggest, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken);
        Task<BidDto> Get(int id, CancellationToken cancellationToken);
    }
}
