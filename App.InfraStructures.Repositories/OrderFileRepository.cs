using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;


namespace App.InfraStructures.Repositories
{
    public class OrderFileRepository : IOrderFileRepository
    {
        public Task Add(OrderFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderFileDto>? Get(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderFileDto>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(OrderFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
