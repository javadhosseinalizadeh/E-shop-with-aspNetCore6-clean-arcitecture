using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class UserFileRepository : IUserFileRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserFileRepository> _logger;

        public UserFileRepository(AppDbContext context, ILogger<UserFileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> Add(UserFileDto userFile, CancellationToken cancellationToken)
        {
            UserFile userFile1 = new()
            {
                FileId = userFile.FileId,
                UserId = userFile.UserId,
                IsDeleted = userFile.IsDeleted
            };
            await _context.UserFiles.AddAsync(userFile1, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("files assigned to user with id {} successfully", userFile.UserId);
            return userFile1.Id;
        }
    }
}
