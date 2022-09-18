using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IAppFileRepository _appFileRepository;
        private readonly IOrderFileRepository _orderFileRepository;
        private readonly IConfiguration _configuration;
        public FileUploadService(IAppFileRepository appFileRepository, IOrderFileRepository orderFileRepository, IConfiguration configuration)
        {
            _appFileRepository = appFileRepository;
            _orderFileRepository = orderFileRepository;
            _configuration = configuration;
        }
        public async Task DeletePhysicalFile(string fileName, CancellationToken cancellationToken)
        {
            var rootpath = _configuration.GetSection("UploadPath").Value;
            File.Delete(Path.Combine(rootpath, fileName));
        }

        public async Task<AppFileDto> Get(int id, CancellationToken cancellationToken)
        {
            var file = await _appFileRepository.Get(id, cancellationToken);
            return file;
        }

        public async Task<List<int>> UploadFileAsync(List<IFormFile> files, CancellationToken cancellationToken)
        {
            List<int> fileIds = new();
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var randomName = Guid.NewGuid().ToString();
                var uniqePath = randomName + "-" + fileName;
                var rootPath = _configuration.GetSection("UploadPath").Value;
                var fullfilePath = Path.Combine(rootPath, uniqePath);
                AppFileDto newFile = new()
                {
                    Path = uniqePath,
                    CreationDate = DateTimeOffset.Now,
                };
                var id = await _appFileRepository.Add(newFile, cancellationToken);
                fileIds.Add(id);
                //var dest = System.IO.File.Create(fullfilePath);

                using (FileStream dest = new FileStream(fullfilePath, FileMode.Create))
                {
                    await file.CopyToAsync(dest, cancellationToken);
                }
            }
            return fileIds;
        }
    }
}
