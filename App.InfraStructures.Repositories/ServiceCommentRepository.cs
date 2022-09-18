using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
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

        public async Task<int> Add(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            var newComment = new ServiceComment()
            {
                Description = comment.Description,
                OrderId = comment.OrderId,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
                IsApproved = comment.IsApproved,
                IsWriteByCustomer = comment.IsWriteByCustomer,
            };
            await _context.ServiceComments.AddAsync(newComment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newComment.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments.SingleAsync(x => x.Id == id, cancellationToken);
            _context.ServiceComments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            var comment1 = await _context.ServiceComments.SingleAsync(x => x.Id == comment.Id, cancellationToken);
            comment1.Title = comment.Title;
            comment1.Description = comment.Description;
            comment1.OrderId = comment.OrderId;
            //comment1.ServiceId = comment.ServiceId;
            comment1.IsApproved = comment.IsApproved;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments
                .Where(x => x.Id == id).SingleAsync(cancellationToken);
            var commentDto = new ServiceCommentDto()
            {
                Id = id,
                Description = comment.Description,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
                ShamsiCreationDate = comment.CreationDate.ToShamsi(),
                //ServiceId = comment.ServiceId,
                OrderId = comment.OrderId,
                IsApproved = comment.IsApproved,
            };
            return commentDto;
        }

        public async Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _context.ServiceComments
                .Where(x => x.OrderId == orderId).SingleOrDefaultAsync(cancellationToken);
            var commentDto = new ServiceCommentDto()
            {
                Id = comment.Id,
                Description = comment.Description,
                Title = comment.Title,
                CreationDate = comment.CreationDate,
                ShamsiCreationDate = comment.CreationDate.ToShamsi(),
                //ServiceId = comment.ServiceId,
                OrderId = comment.OrderId,
                IsApproved = comment.IsApproved,
            };
            return commentDto;
        }

        public async Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken)
        {
            IQueryable<ServiceComment> query = _context.ServiceComments;
            if (approve == 1) { }
            if (approve == 2) { query = query.Where(x => x.IsApproved == true); }
            if (approve == 3) { query = query.Where(x => x.IsApproved == null); }
            if (approve == 4) { query = query.Where(x => x.IsApproved == false); }
            var comments = await query
                .Select(x => new ServiceCommentDto()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    Description = x.Description,
                    //ServiceId = x.ServiceId,
                    Title = x.Title,
                    IsApproved = x.IsApproved,
                })
                .ToListAsync(cancellationToken);
            return comments;
        }

        public async Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments = await _context.Orders.Where(x => x.Id == OrderId)
                .SelectMany(s => s.Comments)
                .Select(x => new ServiceCommentDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    OrderId = x.OrderId,
                    //ServiceId=x.ServiceId,
                    Description = x.Description,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    IsApproved = x.IsApproved,
                })
                .ToListAsync(cancellationToken);
            return comments;
        }
    }
}
