using App.Domain.Core.Contracts.Repositories;
using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace App.InfraStructures.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDto> Get(int id, CancellationToken cancellationToken)
        {
            var orderDto = await _context.Orders
                .Where(x => x.Id == id)
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    StatusName = x.Status.Name,
                    CategoryId = x.Service.CategoryId,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    HomeAddress = x.Customer.HomeAddress,
                    Suggests = x.ExpertSuggests.Select(h => new BidDto()
                    {
                        Id = h.Id,
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        ExpertId = h.ExpertId,
                        IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                        OrderId = h.OrderId,
                        SuggestedPrice = h.SuggestedPrice,
                        ExpertName = h.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(h => new ServiceCommentDto()
                    {
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        OrderId = h.OrderId,
                        ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new AppFileDto()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        Path = z.Path,
                    }).ToList(),

                })
                .SingleAsync(cancellationToken);

            return orderDto;
        }


        public async Task<List<OrderDto>> GetAll(int id, CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _context.Orders;
            if (id == 1)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == false && x.StatusId == 1);
            }
            if (id == 2)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == true && (x.StatusId == 2 || x.StatusId == 3 || x.StatusId == 4));
            }
            if (id == 3)
            {
                query = query.Where(x => x.IsConfirmedByCustomer == true && x.StatusId == 5);
            }
            var orders = await query
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    FinalPrice = x.FinalPrice,
                    ConfirmedExpertId = x.ConfirmedExpertId,
                    CreationDate = x.CreationDate,
                    ShamsiCreationDate = x.CreationDate.ToShamsi(),
                    CustomerId = x.CustomerId,
                    IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                    StatusId = x.StatusId,
                    ServiceId = x.ServiceId,
                    IsDeleted = x.IsDeleted,
                    CategoryId = x.Service.CategoryId,
                    StatusName = x.Status.Name,
                    StatusValue = x.Status.StatusValue,
                    CustomerName = x.Customer.UserName,
                    ExpertName = x.Expert.UserName,
                    ServiceName = x.Service.Title,
                    HomeAddress = x.Customer.HomeAddress,
                    Suggests = x.ExpertSuggests.Select(h => new BidDto()
                    {
                        Id = h.Id,
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        ExpertId = h.ExpertId,
                        IsConfirmedByCustomer = h.IsConfirmedByCustomer,
                        OrderId = h.OrderId,
                        SuggestedPrice = h.SuggestedPrice,
                        ExpertName = h.Expert.UserName,
                    }).ToList(),
                    Comments = x.Comments.Select(h => new ServiceCommentDto()
                    {
                        CreationDate = h.CreationDate,
                        Description = h.Description,
                        Id = h.Id,
                        IsApproved = h.IsApproved,
                        OrderId = h.OrderId,
                        //ServiceId = x.ServiceId,
                        Title = h.Title,
                        IsWriteByCustomer = h.IsWriteByCustomer,
                        ServiceId = x.ServiceId,


                    }).ToList(),
                    Photos = x.OrderFiles.Select(f => f.File).Select(z => new AppFileDto()
                    {
                        CreationDate = z.CreationDate,
                        Id = z.Id,
                        Path = z.Path,
                    }).ToList(),
                })
                .ToListAsync(cancellationToken);

            return orders;
        }

        public async Task<List<OrderDto>> GetAllExpertOrders(AppUserDto expert, string query, CancellationToken cancellationToken)
        {
            IQueryable<Order> queri = _context.Orders;

            //queri = queri.Where(x => expert.expertCategories.Any(d => d.Id == x.Service.CategoryId) && x.StatusId==1 && x.IsConfirmedByCustomer == false);
            if (query == "newest")
            {
                queri = queri.Where(x => expert.expertCategories.Select(x => x.Id).Contains(x.Service.CategoryId) && x.StatusId == 1 && x.IsConfirmedByCustomer == false);
            }
            else if (query == "suggest")
            {
                queri = queri.Where(x => x.ExpertSuggests.Any(d => d.ExpertId == expert.Id) && x.IsConfirmedByCustomer == false && (x.StatusId == 1));
            }
            else if (query == "current")
            {
                queri = queri.Where(x => x.ConfirmedExpertId == expert.Id && x.IsConfirmedByCustomer == true && (x.StatusId == 2 || x.StatusId == 3 || x.StatusId == 4));
            }
            else if (query == "finished")
            {
                queri = queri.Where(x => x.ConfirmedExpertId == expert.Id && x.IsConfirmedByCustomer == true && x.StatusId == 5);
            }
            else
            {
                return new List<OrderDto>();
            }
            var orders = await queri.Select(x => new OrderDto()
            {
                FinalPrice = x.FinalPrice,
                ConfirmedExpertId = x.ConfirmedExpertId,
                StatusId = x.StatusId,
                CreationDate = x.CreationDate,
                CustomerId = x.CustomerId,
                IsConfirmedByCustomer = x.IsConfirmedByCustomer,
                ServiceId = x.ServiceId,
                Id = x.Id,
                CustomerName = x.Customer.UserName,
                ServiceName = x.Service.Title,
                StatusName = x.Status.Name,
                StatusValue = x.Status.StatusValue,
                ShamsiCreationDate = x.CreationDate.ToShamsi(),
            }).ToListAsync(cancellationToken);
            return orders;
        }
        public async Task<List<AppFileDto>> GetAllFiles(int orderId, CancellationToken cancellationToken)
        {
            var files = await _context.OrderFiles.Where(x => x.OrderId == orderId).Select(x => x.File).Select(x => new AppFileDto()
            {
                Id = x.Id,
                Path = x.Path,
                CreationDate = x.CreationDate,
            }).ToListAsync(cancellationToken);
            return files;
        }
        public async Task<int> Add(OrderDto order, CancellationToken cancellationToken)
        {
            var newOrder = new Order()
            {

                StatusId = order.StatusId,
                ConfirmedExpertId = order.ConfirmedExpertId,
                CustomerId = order.CustomerId,
                Description = order.Description,
                FinalPrice = order.FinalPrice,
                IsConfirmedByCustomer = order.IsConfirmedByCustomer,
                CreationDate = order.CreationDate,
                IsDeleted = order.IsDeleted,
                ServiceId = order.ServiceId,

            };
            await _context.Orders.AddAsync(newOrder, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newOrder.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _context.Orders.SingleAsync(x => x.Id == id, cancellationToken);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("امکان حذف به دلیل استفاده شناسه وجود ندارد", ex.InnerException);
            }
        }

        public async Task DeleteOrderFile(int id, CancellationToken cancellationToken)
        {
            var orderFile = await _context.Files.SingleAsync(x => x.Id == id, cancellationToken);
            _context.Remove(orderFile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(OrderDto order, CancellationToken cancellationToken)
        {
            var order1 = await _context.Orders.SingleAsync(x => x.Id == order.Id, cancellationToken);
            order1.ConfirmedExpertId = order.ConfirmedExpertId;
            order1.IsConfirmedByCustomer = order.IsConfirmedByCustomer;
            order1.Description = order.Description;
            order1.FinalPrice = order.FinalPrice;
            order1.StatusId = order.StatusId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
