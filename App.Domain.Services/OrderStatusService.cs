using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;


namespace App.Domain.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }
        public async Task<int> Add(OrderStatusDto statusDTO, CancellationToken cancellationToken)
        {
            statusDTO.CreationDate = DateTimeOffset.Now;
            var result = await _orderStatusRepository.Add(statusDTO, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderStatusRepository.Delete(id, cancellationToken);
        }

        public async Task EnsureStatusIsNotExist(string name, CancellationToken cancellationToken)
        {
            var service = await _orderStatusRepository.Get(name, cancellationToken);
            if (!(service == null))
                throw new Exception("وضعیت مورد نظر قبلا ایجاد شده است");
        }

        public async Task<OrderStatusDto> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _orderStatusRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken)
        {
            var statuses = await _orderStatusRepository.GetAll(cancellationToken);
            return statuses;
        }

        public async Task Update(OrderStatusDto statusDTO, CancellationToken cancellationToken)
        {
            await _orderStatusRepository.Update(statusDTO, cancellationToken);
        }
    }
}
