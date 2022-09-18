using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface IBidAppService
    {
        Task<int> CreateSuggest(int orderId, int expertId, int price, string description, CancellationToken cancellationToken);
        Task Update(BidDto suggest, CancellationToken cancellationToken);
        Task EditSuggest(int suggestId, int price, string description, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken);
        Task<BidDto> Get(int id, CancellationToken cancellationToken);
    }
}
