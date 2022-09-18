using App.Domain.Core.Contracts.AppServices;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;
        private readonly IUserAppService _userAppService;
        private readonly ILogger<CommentAppService> _logger;

        public CommentAppService(ICommentService commentService, IUserAppService userAppService, ILogger<CommentAppService> logger)
        {
            _commentService = commentService;
            _userAppService = userAppService;
            _logger = logger;
        }

        public async Task<int> Add(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            var result = await _commentService.Add(comment, cancellationToken);
            if (result != 0)
            {
                _logger.LogInformation("new comment {action} successfully", "add");
            }
            else
            {
                _logger.LogWarning("{action} new comment failed", "add");
            }
            return result;
        }

        public async Task ChangeCommentStatus(int commentId, bool status, CancellationToken cancellationToken)
        {
            var comment = await _commentService.Get(commentId, cancellationToken);
            comment.IsApproved = status;
            await _commentService.Update(comment, cancellationToken);
        }

        public async Task<int> CreateOrderComment(int orderId, int serviceId, string title, string description, CancellationToken cancellationToken)
        {
            var currentUser = await _userAppService.GetCurrentUserFullInfo(cancellationToken);

            ServiceCommentDto comment = new()
            {
                CreationDate = DateTimeOffset.Now,
                Description = description,
                IsApproved = null,
                IsDeleted = false,
                OrderId = orderId,
                ServiceId = serviceId,
                Title = title,
                IsWriteByCustomer = false
            };
            if (currentUser.Roles.Count == 1 && currentUser.Roles.Contains("Customer") || currentUser.Roles.Contains("CUSTOMER"))

            {
                comment.IsWriteByCustomer = true;

            }


            var id = await _commentService.Add(comment, cancellationToken);
            _logger.LogInformation("new commment {action} for order with id {id} by {user}", "create", orderId, currentUser.UserName);
            return id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _commentService.Delete(id, cancellationToken);
            _logger.LogInformation("comment {action} successfully", "Delete");
        }

        public async Task<ServiceCommentDto> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _commentService.Get(id, cancellationToken);
            return result;
        }

        public async Task<List<ServiceCommentDto>> GetAll(int approve, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetAll(approve, cancellationToken);
            return comments;
        }

        public async Task<List<ServiceCommentDto>> GetAllOrderComments(int OrderId, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetAllOrderComments(OrderId, cancellationToken);
            return comments;
        }

        public async Task<ServiceCommentDto> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var comment = await _commentService.GetByOrderId(orderId, cancellationToken);
            return comment;
        }

        public async Task Update(ServiceCommentDto comment, CancellationToken cancellationToken)
        {
            await _commentService.Update(comment, cancellationToken);
        }
    }
}
