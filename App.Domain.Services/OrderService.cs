using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly IOrderFileRepository _orderFileRepository;

        public OrderService(IOrderRepository orderRepository, IConfiguration configuration, IOrderFileRepository orderFileRepository)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _orderFileRepository = orderFileRepository;
        }

        public async Task<int> Add(OrderDto order, CancellationToken cancellationToken)
        {
            order.CreationDate = DateTimeOffset.Now;
            order.IsDeleted = false;
            order.StatusId = 1;
            order.ConfirmedExpertId = null;
            order.IsConfirmedByCustomer = false;
            order.Description = order.Description;

            var result = await _orderRepository.Add(order, cancellationToken);
            return result;
        }

        public async Task<bool> AddOrderFiles(int orderId, List<int> fileIds, CancellationToken cancellationToken)
        {
            foreach (var fileId in fileIds)
            {
                OrderFileDto orderFile = new()
                {
                    FileId = fileId,
                    OrderId = orderId,
                };
                var result = await _orderFileRepository.Add(orderFile, cancellationToken);
            }
            return true;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderRepository.Delete(id, cancellationToken);
        }

        public async Task DeleteOrderFile(int id, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteOrderFile(id, cancellationToken);
        }

        public async Task<OrderDto> Get(int id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Get(id, cancellationToken);
            return order;
        }

        public async Task<List<OrderDto>> GetAll(int id, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAll(id, cancellationToken);
            return orders;
        }

        public async Task<List<OrderDto>> GetAllExpertOrders(AppUserDto expert, string query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllExpertOrders(expert, query, cancellationToken);
            return orders;
        }

        public async Task<List<AppFileDto>> GetAllFiles(int orderId, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetSection("DownloadPath").Value;
            var paths = await _orderRepository.GetAllFiles(orderId, cancellationToken);
            foreach (var path in paths)
            {
                path.Path = rootPath + "/" + path.Path;
            }
            return paths;
        }

        public async Task Update(OrderDto order, CancellationToken cancellationToken)
        {
            await _orderRepository.Update(order, cancellationToken);
        }
    }
}
