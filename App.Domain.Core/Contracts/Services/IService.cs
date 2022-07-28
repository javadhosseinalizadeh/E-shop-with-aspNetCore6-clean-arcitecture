using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.Services
{
    public interface IService
    {
        Task SetProfileImg(Guid userId, FileInfo file);
    }
}
