using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.InfraStructures.Database.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class AppFileRepository : IAppFileRepository
    {
        public Task Add(AppFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AppFileDto>? Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AppFileDto>? Get(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(AppFileDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
