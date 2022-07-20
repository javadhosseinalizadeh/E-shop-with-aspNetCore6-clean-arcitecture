using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IExpertFavoriteCategoryRepository
    {

        #region "Queries"

        Task<ExpertFavoriteCategoryDto>? Get(int id,CancellationToken cancellationToken);

        #endregion



        #region "Commands"

        Task Add(ExpertFavoriteCategoryDto dto, CancellationToken cancellationToken);
        Task Update(ExpertFavoriteCategoryDto dto, CancellationToken cancellationToken);
        Task Delete(int id,CancellationToken cancellationToken);

        #endregion
    }
}
