using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface ICategoryRepository
    {

        #region "Queries"

        Task<CategoryDto>? Get(int id, CancellationToken cancellationToken);
        Task<CategoryDto>? Get(string title, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAllWithServices(CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task<int> Add(CategoryDto dto, CancellationToken cancellationToken);
        Task Update(CategoryDto dto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);

        #endregion
    }
}
