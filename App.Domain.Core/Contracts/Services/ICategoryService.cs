using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAllWithServices(CancellationToken cancellationToken);
        Task Set(CategoryDto dto, CancellationToken cancellationToken);
        Task<CategoryDto> Get(int id, CancellationToken cancellationToken);
        Task<CategoryDto> Get(string name, CancellationToken cancellationToken);
        Task Update(CategoryDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task EnsureDoesNotExist(string name, CancellationToken cancellationToken);
        Task EnsureExists(string name, CancellationToken cancellationToken);
        Task EnsureExists(int id, CancellationToken cancellationToken);
    }
}
