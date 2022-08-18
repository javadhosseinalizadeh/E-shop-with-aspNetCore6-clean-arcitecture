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
        Task<bool> UploadFile(IFormFile file);
    }
}
