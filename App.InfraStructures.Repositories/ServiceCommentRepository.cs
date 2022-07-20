using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Dtos;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.InfraStructures.Repositories
{
    public class ServiceCommentRepository : IServiceCommentRepository
    {
        private readonly AppDbContext _context;
        public ServiceCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            App.Domain.Core.Entities.ServiceComment comment = new()
            {
                ServiceId = dto.ServiceId,
                OrderId = dto.OrderId,
                CommentText = dto.CommentText,
                CreatedUserId = dto.CreatedUserId,
                CreatedAt = dto.CreatedAt,
            };
            await _context.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments.Where(c => c.Id == id).SingleAsync(cancellationToken);
            _context.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ServiceCommentDto>? Get(int id, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments.Where(c => c.Id == id).Select(c => new ServiceCommentDto()
            {
                Id = c.Id,
                ServiceId = c.ServiceId,
                OrderId = c.OrderId,
                CommentText = c.CommentText,
                CreatedUserId = c.CreatedUserId,
                CreatedAt = c.CreatedAt,
            }).SingleOrDefaultAsync(cancellationToken);
            return comment;
        }



        public async Task Update(ServiceCommentDto dto, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments.Where(c => c.Id == dto.Id).SingleAsync(cancellationToken);
            comment.ServiceId = dto.ServiceId;
            comment.OrderId = dto.OrderId;
            comment.CreatedUserId = dto.CreatedUserId;
            comment.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
