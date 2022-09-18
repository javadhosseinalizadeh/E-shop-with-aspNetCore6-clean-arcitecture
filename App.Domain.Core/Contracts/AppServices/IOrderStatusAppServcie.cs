﻿using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AppServices
{
    public interface IOrderStatusAppServcie
    {
        Task<List<OrderStatusDto>> GetAll(CancellationToken cancellationToken);
        Task<int> Add(OrderStatusDto statusDTO, CancellationToken cancellationToken);
        Task<OrderStatusDto> Get(int id, CancellationToken cancellationToken);
        Task Update(OrderStatusDto statusDTO, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
