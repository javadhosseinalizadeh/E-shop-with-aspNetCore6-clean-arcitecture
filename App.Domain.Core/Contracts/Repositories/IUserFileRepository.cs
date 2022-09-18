using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Repositories
{
    public interface IUserFileRepository
    {
        Task<int> Add(UserFileDto userFile, CancellationToken cancellationToken);

    }
}
