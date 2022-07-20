using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class ServiceFileRepository : IServiceFileRepository
    {
        public Task Add(ServiceFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceFileDto>? Get(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<ServiceFileDto>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(ServiceFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
