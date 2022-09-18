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
    public class OrderStatusAppService : IOrderStatusAppServcie
    {
        private readonly IOrderStatusService _orderStatusService;
        private readonly ILogger<OrderStatusAppService> _logger;

        public OrderStatusAppService(IOrderStatusService orderStatusService, ILogger<OrderStatusAppService> logger)
        {
            _orderStatusService = orderStatusService;
            _logger = logger;
        }

        public async Task<int> Add(OrderStatusDto status, CancellationToken cancellationToken)
        {
            await _orderStatusService.EnsureStatusIsNotExist(status.Name, cancellationToken);
            var result = await _orderStatusService.Add(status, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new status {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new status failed", "add");
            }
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderStatusService.Delete(id, cancellationToken);
            _logger.LogInformation("status with id {id} {action} successfully", id, "Delete");
        }

        public Task<OrderStatusDto> Get(int id, CancellationToken cancellationToken)
        {
            var result = _orderStatusService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _orderStatusService.GetAll(cancellationToken);
            return statuses;
        }

        public async Task Update(OrderStatusDto category, CancellationToken cancellationToken)
        {
            await _orderStatusService.Update(category, cancellationToken);
        }
    }
}
