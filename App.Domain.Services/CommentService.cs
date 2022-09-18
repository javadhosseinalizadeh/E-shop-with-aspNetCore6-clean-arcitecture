using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IServiceCommentRepository _serviceCommentRepository;
        public CommentService(IServiceCommentRepository serviceCommentRepository)
        {
            _serviceCommentRepository = serviceCommentRepository;
        }
        public async Task<int> Add(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            comment.CreationDate = DateTimeOffset.Now;
            comment.IsDeleted = false;
            comment.IsApproved = null;
            var result = await _serviceCommentRepository.Add(comment, cancellationToken);
            return result;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Delete(id, cancellationToken);
        }

        public async Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _serviceCommentRepository.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken)
        {
            var comments = await _serviceCommentRepository.GetAll(approve, cancellationToken);
            return comments;
        }

        public async Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments = await _serviceCommentRepository.GetAllOrderComments(OrderId, cancellationToken);
            return comments;
        }

        public async Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _serviceCommentRepository.GetByOrderId(orderId, cancellationToken);
            return comment;
        }

        public async Task Update(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            await _serviceCommentRepository.Update(comment, cancellationToken);
        }
    }
}
