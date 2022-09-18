using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface IFileUploadService
    {
        Task<AppFileDto> Get(int id, CancellationToken cancellationToken);
        Task<List<int>> UploadFileAsync(List<IFormFile> files, CancellationToken cancellationToken);
        Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken);
    }
}
