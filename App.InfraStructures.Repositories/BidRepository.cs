
using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace App.InfraStructures.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _context;
        public BidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(BidDto dto, CancellationToken cancellationToken)
        {
            var bid = new Bid()
            {
                OrderId = dto.OrderId,
                ExpertId = dto.ExpertId,
                SuggestedPrice = dto.SuggestedPrice,
                IsConfirmedByCustomer = dto.IsConfirmedByCustomer,
                CreationDate = dto.CreationDate,
            };
            await _context.Bids.AddAsync(bid, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return bid.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var bid = await _context.Bids.Where(b => b.Id == id).SingleAsync(cancellationToken);
            _context.Remove(bid);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BidDto>? Get(int id, CancellationToken cancellationToken)
        {
            var expertSuggest = await _context.Bids
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var expertBidDto = new BidDto()
            {
                Id = id,
                CreationDate = expertSuggest.CreationDate,
                ShamsiCreationDate = expertSuggest.CreationDate.ToShamsi(),
                ExpertId = expertSuggest.ExpertId,
                IsConfirmedByCustomer = expertSuggest.IsConfirmedByCustomer,
                OrderId = expertSuggest.OrderId,
                SuggestedPrice = expertSuggest.SuggestedPrice,
                Description = expertSuggest.Description,
            };
            return expertBidDto;
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            var expertBids = await _context.Bids
                 .Select(x => new BidDto()
                 {
                     Id = x.Id,
                     CreationDate = x.CreationDate,
                     ShamsiCreationDate = x.CreationDate.ToShamsi(),
                     ExpertId = x.ExpertId,
                     IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                     OrderId = x.OrderId,
                     SuggestedPrice = x.SuggestedPrice
                 })
                 .ToListAsync(cancellationToken);
            return expertBids;
        }

        public async Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var expertSuggests = await _context.Bids
                .Where(x => x.OrderId == OrderId)
                .Select(x => new BidDto()
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    ExpertId = x.ExpertId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    OrderId = x.OrderId,
                    SuggestedPrice = x.SuggestedPrice,
                    Description = x.Description,
                    ExpertName = x.Expert.FirstName,

                })
                .ToListAsync(cancellationToken);
            return expertSuggests;
        }
        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            var bid = await _context.Bids.Where(b => b.Id == dto.Id).SingleAsync(cancellationToken);
            // bid.OrderId = dto.OrderId;
            // bid.ExpertId = dto.ExpertId;
            bid.SuggestedPrice = dto.SuggestedPrice;
            bid.IsConfirmedByCustomer = dto.IsConfirmedByCustomer;
            bid.Description = dto.Description;
            //  _context.Update(bid);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
