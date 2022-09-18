using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        public async Task<int> Add(BidDto suggest, CancellationToken cancellationToken)
        {
            var result = await _bidRepository.Add(suggest, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidRepository.Delete(id, cancellationToken);
        }

        public async Task<BidDto> Get(int id, CancellationToken cancellationToken)
        {
            var suggest = await _bidRepository.Get(id, cancellationToken);
            return suggest;
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            var suggests = await _bidRepository.GetAll(cancellationToken);
            return suggests;
        }

        public async Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var suggests = await _bidRepository.GetAll(OrderId, cancellationToken);
            return suggests;
        }

        public async Task Update(BidDto suggest, CancellationToken cancellationToken)
        {
            await _bidRepository.Update(suggest, cancellationToken);
        }
    }
}
