
using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
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

        public async Task Add(BidDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.Bid bid = new()
            {
                OrderId = dto.OrderId,
                ExpertUserId = dto.ExpertUserId,
                SuggestedPrice = dto.SuggestedPrice,
                IsApproved = dto.IsApproved,
                CreatedAt = dto.CreatedAt,
            };
            await _context.AddAsync(bid, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var bid = await _context.Bids.Where(b => b.Id == id).SingleAsync(cancellationToken);
            _context.Remove(bid);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BidDto>? Get(int id, CancellationToken cancellationToken)
        {
            var bid = await _context.Bids.Where(b => b.Id == id).Select(b => new BidDto()
            {
                Id = b.Id,
                OrderId = b.OrderId,
                ExpertUserId = b.ExpertUserId,
                SuggestedPrice = b.SuggestedPrice,
                IsApproved = b.IsApproved,
                CreatedAt = b.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return bid;
        }

        public async Task Update(BidDto dto, CancellationToken cancellationToken)
        {
            var bid = await _context.Bids.Where(b => b.Id == dto.Id).SingleAsync(cancellationToken);
            bid.OrderId = dto.OrderId;
            bid.ExpertUserId = dto.ExpertUserId;
            bid.SuggestedPrice = dto.SuggestedPrice;
            bid.IsApproved = dto.IsApproved;
            bid.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
