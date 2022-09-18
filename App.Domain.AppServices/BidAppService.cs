using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class BidAppService : IBidAppService
    {
        private readonly IBidService _bidService;
        private readonly ILogger<BidAppService> _logger;
        public BidAppService(IBidService bidService, ILogger<BidAppService> logger)
        {
            _bidService = bidService;
            _logger = logger;
        }
        public async Task<int> CreateSuggest(int orderId, int expertId, int price, string description, CancellationToken cancellationToken)
        {
            BidDto suggest = new()
            {
                SuggestedPrice = price,
                CreationDate = DateTimeOffset.Now,
                Description = description,
                ExpertId = expertId,
                IsConfirmedByCustomer = false,
                OrderId = orderId,
            };
            var result = await _bidService.Add(suggest, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new suggest {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new suggest failed", "add");
            }
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidService.Delete(id, cancellationToken);
            _logger.LogInformation("suggest with id {id} {action} successfully", id, "Delete");
        }

        public async Task EditSuggest(int suggestId, int price, string description, CancellationToken cancellationToken)
        {
            var suggest = await _bidService.Get(suggestId, cancellationToken);
            suggest.Description = description;
            suggest.SuggestedPrice = price;
            await _bidService.Update(suggest, cancellationToken);

        }

        public async Task<BidDto> Get(int id, CancellationToken cancellationToken)
        {
            var suggest = await _bidService.Get(id, cancellationToken);
            return suggest;
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            var suggests = await _bidService.GetAll(cancellationToken);
            return suggests;
        }

        public async Task<List<BidDto>> GetAll(int OrderId, CancellationToken cancellationToken)
        {
            var suggests = await _bidService.GetAll(OrderId, cancellationToken);
            return suggests;
        }

        public async Task Update(BidDto suggest, CancellationToken cancellationToken)
        {
            await _bidService.Update(suggest, cancellationToken);
        }
    }
}
